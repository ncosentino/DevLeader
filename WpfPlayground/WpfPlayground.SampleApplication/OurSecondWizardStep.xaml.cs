using System.Windows.Controls;

namespace WpfPlayground.SampleApplication;

public partial class OurSecondWizardStep : 
    UserControl,
    IWizardStepView
{
    public OurSecondWizardStep(OurSecondWizardStepViewModel viewModel)
    {
        DataContext = viewModel;
        ViewModel = viewModel;
        InitializeComponent();
    }

    public IWizardStepViewModel ViewModel { get; }
}

public sealed class OurSecondWizardStepViewModel :
    BaseWizardStepViewModel
{
    public OurSecondWizardStepViewModel()
    {
        CanGoNext = true;
        CanGoBack = true;
    }

    public override IWizardStepView? GetNextStep() => null;
}