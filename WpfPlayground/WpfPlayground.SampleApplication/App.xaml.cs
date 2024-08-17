using Microsoft.Extensions.DependencyInjection;

using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfPlayground.SampleApplication;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        // FIXME: do more robust checking than this
        // naive approach to blindly try and load
        // any and all assemblies with no checks!
        var assemblies = Directory
            .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(Assembly.LoadFrom)
            .ToArray();

        ServiceCollection serviceCollection = new();
        serviceCollection.Scan(x => x
            .FromAssemblies(assemblies)
            .AddClasses()
            .AsSelfWithInterfaces()
            .WithSingletonLifetime());
        serviceCollection.AddSingleton((sp) => Current.Dispatcher);
        serviceCollection.AddSingleton(sp => StitchingDependencies.CreateStitcherFactory());

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        this.Properties["ServiceProvider"] = _serviceProvider;

        var mainPresenter = _serviceProvider.GetRequiredService<MainWindowPresenter>();
        var firstStep = _serviceProvider.GetRequiredService<OurFirstWizardStep>();
        //var firstStep = new OurFirstWizardStep(new WizardStepViewModel());
        //mainPresenter.Show(firstStep);

        var splashPresenter = _serviceProvider.GetRequiredService<SplashPresenter>();

        await splashPresenter.ShowSplashAsync(
            maximumSplashTime: TimeSpan.FromSeconds(5),
            doHeavyWorkAsyncCallback: async (progress, cancellationToken) =>
            {
                Debug.Assert(
                    Thread.CurrentThread != Current.Dispatcher.Thread,
                    "Background work needs to be done... on the background thread!");
                for (int i = 0; i < 50; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    SplashScreenProgress progressInfo = new(
                        (i + 1) * 0.02,
                        $"Doing work {i + 1} of 50");
                    progress.Report(progressInfo);

                    await Task.Delay(50, cancellationToken);
                }
            },
            cancellationToken: CancellationToken.None);
    }
}

public sealed record SplashScreenProgress(
    double Value,
    string Message);

public interface ISplashScreen
{
    SplashViewModel ViewModel { get; }
}

public sealed class MainWindowPresenter
{
    private readonly MainWindow _mainWindow;
    private readonly WizardPresenter _wizardPresenter;

    public MainWindowPresenter(
        MainWindow mainWindow,
        WizardPresenter wizardPresenter)
    {
        _mainWindow = mainWindow;
        _wizardPresenter = wizardPresenter;
    }

    public void Show(IWizardStepView? firstStep)
    {
        _mainWindow.Show();
        _wizardPresenter.NavigateToStep(firstStep);
    }
}
