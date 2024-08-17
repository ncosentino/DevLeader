using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfPlayground.SampleApplication;

public partial class WizardControl : UserControl
{
    public WizardControl(
        WizardControlViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}

public interface IWizardStepView
{
    IWizardStepViewModel ViewModel { get; }
}

public interface IWizardStepViewModel : INotifyPropertyChanged
{
    bool CanGoNext { get; }

    bool CanGoBack { get; }

    IWizardStepView? GetNextStep();
}

public sealed class WizardPresenter
{
    private readonly WizardControlViewModel _wizardControlViewModel;
    private readonly Stack<IWizardStepView> _stepHistory;

    public WizardPresenter(WizardControlViewModel wizardControlViewModel)
    {
        _stepHistory = new();

        _wizardControlViewModel = wizardControlViewModel;
        _wizardControlViewModel.BackRequested += WizardControlViewModel_BackRequested;
        _wizardControlViewModel.NextRequested += WizardControlViewModel_NextRequested;
    }

    public void NavigateToStep(IWizardStepView? wizardStepView)
        => GoToStep(wizardStepView, trackHistory: true);

    private void GoToStep(
        IWizardStepView? wizardStepView, 
        bool trackHistory)
    {
        if (trackHistory && _wizardControlViewModel.CurrentWizardStepView is not null)
        {
            _stepHistory.Push(_wizardControlViewModel.CurrentWizardStepView);
        }

        _wizardControlViewModel.CurrentWizardStepView = wizardStepView;
        _wizardControlViewModel.CanGoNext = wizardStepView?.ViewModel.CanGoNext ?? false;
        _wizardControlViewModel.CanGoBack = wizardStepView?.ViewModel.CanGoBack ?? false;

        if (wizardStepView is null)
        {
            // exit condition?????
        }
    }

    private void WizardControlViewModel_NextRequested(
        object? sender,
        EventArgs e)
    {
        Debug.Assert(_wizardControlViewModel.CurrentWizardStepView != null);
        var currentStepView = _wizardControlViewModel.CurrentWizardStepView;
        var currentStepViewModel = currentStepView?.ViewModel;

        var nextStepView = currentStepViewModel.GetNextStep();
        NavigateToStep(nextStepView);
    }

    private void WizardControlViewModel_BackRequested(
        object? sender,
        EventArgs e)
    {
        Debug.Assert(_stepHistory.Count > 0);
        var previousStep = _stepHistory.Pop();
        GoToStep(previousStep, trackHistory: false);
    }
}

public sealed class WizardControlViewModel : INotifyPropertyChanged
{
    private bool _canGoBack;
    private bool _canGoNext;
    private IWizardStepView? _currentWizardStepView;

    public WizardControlViewModel()
    {
        BackCommand = new RelayCommand(_ => BackRequested?.Invoke(this, EventArgs.Empty));
        NextCommand = new RelayCommand(_ => NextRequested?.Invoke(this, EventArgs.Empty));
    }

    public event EventHandler? BackRequested;

    public event EventHandler? NextRequested;

    public event PropertyChangedEventHandler? PropertyChanged;

    public IWizardStepView? CurrentWizardStepView
    { 
        get => _currentWizardStepView; 
        set 
        {
            if (_currentWizardStepView != value)
            {
                if (_currentWizardStepView?.ViewModel != null)
                {
                    _currentWizardStepView.ViewModel.PropertyChanged -= CurrentStepViewModel_PropertyChanged;
                }

                _currentWizardStepView = value;

                if (_currentWizardStepView?.ViewModel != null)
                {
                    _currentWizardStepView.ViewModel.PropertyChanged += CurrentStepViewModel_PropertyChanged;
                }

                OnPropertyChanged();
            }
        } 
    }

    private void CurrentStepViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var stepViewModel = (IWizardStepViewModel)sender!;
        if (e.PropertyName == nameof(IWizardStepViewModel.CanGoNext))
        {
            CanGoNext = stepViewModel.CanGoNext;
        }
        else if (e.PropertyName == nameof(IWizardStepViewModel.CanGoBack))
        {
            CanGoBack = stepViewModel.CanGoBack;
        }
    }

    public bool CanGoBack
    {
        get => _canGoBack;
        set
        {
            if (_canGoBack != value)
            {
                _canGoBack = value;
                OnPropertyChanged();
            }
        }
    }

    public bool CanGoNext
    {
        get => _canGoNext;
        set
        {
            if (_canGoNext != value)
            {
                _canGoNext = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand BackCommand { get; }

    public ICommand NextCommand { get; }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public sealed class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(
        Action<object?> execute,
        Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}