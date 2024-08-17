using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace WpfPlayground.SampleApplication;

public sealed class SplashPresenter
{
    private readonly SplashViewModel _splashViewModel;
    private readonly MainWindowPresenter _mainWindowPresenter;
    private readonly Dispatcher _dispatcher;

    public SplashPresenter(
        ISplashScreen splashScreen,
        MainWindowPresenter mainWindowPresenter,
        Dispatcher dispatcher)
    {
        _splashViewModel = splashScreen.ViewModel;
        _mainWindowPresenter = mainWindowPresenter;
        _dispatcher = dispatcher;
    }

    internal async Task ShowSplashAsync(
        TimeSpan maximumSplashTime,
        Func<IProgress<SplashScreenProgress>, CancellationToken, Task> doHeavyWorkAsyncCallback,
        CancellationToken cancellationToken)
    {
        var minWaitCancelTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(cancellationToken);

        _splashViewModel.Closing += (sender, e) =>
        {
            minWaitCancelTokenSource.Cancel();
            _mainWindowPresenter.Show(null);
        };
        _splashViewModel.OpenCommand.Execute(null);

        Task maximumWaitTask = Task
            .Delay(
                maximumSplashTime,
                minWaitCancelTokenSource.Token)
            .ContinueWith(t =>
            {
                _dispatcher.Invoke(() =>
                {
                    _splashViewModel.CloseCommand.Execute(null);
                });
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);

        var progressReporter = _splashViewModel;
        await Task
            .Run(async () =>
            {
                Debug.Assert(
                    Thread.CurrentThread != _dispatcher.Thread,
                    "Background work needs to be done... on the background thread!");
                await doHeavyWorkAsyncCallback
                    .Invoke(progressReporter, cancellationToken)
                    .ConfigureAwait(false);
            },
            cancellationToken);

        _splashViewModel.CanClose = true;

        try
        {
            await maximumWaitTask;
        }
        catch (OperationCanceledException)
        {
        }
    }
}
