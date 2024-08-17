using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace WpfPlayground.SampleApplication;

public partial class SplashWindow :
    Window,
    ISplashScreen
{
    public SplashWindow(SplashViewModel viewModel)
    {
        this.ViewModel = viewModel;
        DataContext = viewModel;
        InitializeComponent();

        viewModel.RequestClose += (sender, e) => Close();
        viewModel.RequestOpen += (sender, e) => Show();
    }

    public SplashViewModel ViewModel { get; }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        ViewModel.ClosingCommand.Execute(null);
    }
}

public sealed class SplashViewModel : 
    INotifyPropertyChanged,
    IProgress<SplashScreenProgress>
{
    private string? _progressText;
    private double _progressValue;
    private bool _canClose;

    public SplashViewModel()
    {
        CloseCommand = new RelayCommand(_ => RequestClose?.Invoke(
            this, 
            EventArgs.Empty));
        ClosingCommand = new RelayCommand(_ => Closing?.Invoke(
            this,
            EventArgs.Empty));
        OpenCommand = new RelayCommand(_ => RequestOpen?.Invoke(
            this,
            EventArgs.Empty));
    }

    public event EventHandler? Closing;

    public event EventHandler? RequestClose;

    public event EventHandler? RequestOpen;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand CloseCommand { get; }

    public ICommand ClosingCommand { get; }

    public ICommand OpenCommand { get; }

    public string? ProgressText
    {
        get => _progressText;
        set
        {
            if (_progressText != value)
            {
                _progressText = value;
                OnPropertyChanged();
            }
        }
    }

    public double ProgressValue
    {
        get => _progressValue;
        set
        {
            if (_progressValue != value)
            {
                _progressValue = value;
                OnPropertyChanged();
            }
        }
    }

    public bool CanClose
    {
        get => _canClose;
        set
        {
            if (_canClose != value)
            {
                _canClose = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(WindowStyle));
            }
        }
    }

    public WindowStyle WindowStyle => CanClose
        ? WindowStyle.ToolWindow
        : WindowStyle.None;

    public void Report(SplashScreenProgress value)
    {
        ProgressText = value.Message;
        ProgressValue = value.Value;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}