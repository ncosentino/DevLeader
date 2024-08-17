using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace WpfPlayground.SampleApplication;

public partial class OurFirstWizardStep : 
    UserControl,
    IWizardStepView
{
    public OurFirstWizardStep(
        OurFirstWizardStepViewModel viewModel)
    {
        DataContext = viewModel;
        ViewModel = viewModel;
        InitializeComponent();
    }

    public IWizardStepViewModel ViewModel { get; }
}


public sealed class OurFirstWizardStepViewModel : 
    BaseWizardStepViewModel
{
    private readonly IWizardStepView _nextStep;

    public OurFirstWizardStepViewModel(OurSecondWizardStep nextStep)
    {
        _nextStep = nextStep;
        CanGoBack = false;
    }

    public override IWizardStepView? GetNextStep() => _nextStep;
}

public abstract class BaseWizardStepViewModel :
    IWizardStepViewModel
{
    private bool _canGoNext;
    private bool _canGoBack;

    public event PropertyChangedEventHandler? PropertyChanged;

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

    public abstract IWizardStepView? GetNextStep();

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}