using Microsoft.Extensions.DependencyInjection;

using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfPlayground.SampleApplication;

public partial class MainWindow : Window
{
    public MainWindow(
        StitcherFactory stitcherFactory,
        WizardControl wizardControl)
    {
        InitializeComponent();

        stitcherFactory
            .Create<ISimpleStitcher>(WizardContent, wizardControl)
            .Stitch();
    }
}

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    private bool _isWizardVisible = false;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsWizardVisible
    { 
        get => _isWizardVisible; 
        set 
        {
            if (_isWizardVisible != value)
            {
                _isWizardVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


[ValueConversion(typeof(bool), typeof(Visibility))]
public class CustomBooleanToVisibilityConverter : 
    MarkupExtension,
    IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object Convert(
        object value, 
        Type targetType,
        object parameter, 
        CultureInfo culture)
    {
        if (value is bool booleanValue)
        {
            return booleanValue ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(
        object value,
        Type targetType, 
        object parameter,
        CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }

        return false;
    }
}

public sealed class ViewModelProvider(
    Type _viewModelType) :
    MarkupExtension
{    
    public override object ProvideValue(
        IServiceProvider _)
    {
        var provider = (IServiceProvider?)Application
            .Current
            .Properties["ServiceProvider"];
        ArgumentNullException.ThrowIfNull(
            provider, 
            nameof(provider));

        var viewModel = provider.GetRequiredService(_viewModelType);
        return viewModel;
    }
}