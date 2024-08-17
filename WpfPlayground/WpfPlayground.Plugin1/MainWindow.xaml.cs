using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using WpfPlayground.Converters;
using WpfPlayground.Sdk;

namespace WpfPlayground.SampleApplication;

public partial class MainWindow : 
    Window,
    IMainWindow
{
    public MainWindow(
        MainWindowViewModel viewModel,
        NicksCoolConverter nicksCoolConverter,
        StitcherFactory sticherFactory,
        NicksCustomControl customControl)
    {
        InitializeComponent();
        DataContext = viewModel;

        Binding binding = new(nameof(MainWindowViewModel.CoolLevel))
        {
            Converter = nicksCoolConverter,
        };
        CoolLabel.SetBinding(ContentProperty, binding);

        // FIXME: why is this code not compiling?!
        var GridContainer = new Grid();
        var stackPanel = new StackPanel();
        var textBlock = new TextBlock
        {
            Text = "This is a dynamically added TextBlock!",
            FontSize = 24,
        };
        sticherFactory
            .Create<IGridSticher>(stackPanel, textBlock)
            .Stitch(row: 123, column: 456);

        GridContainer.Children.Add(new TextBlock
        {
            Text = "This is a dynamically added TextBlock!",
            FontSize = 24,
        });
        GridContainer.Children.Add(new Html(
        """
        <head>
            <style>
            h1 {
                color: red;
            }
            </style>
        </head>
        """));
    }
}

public sealed class MainWindowViewModel
{
    public string CustomTitle { get; } = "Hello, World!";

    public string FancyText { get; set; } = "Fancy!!";

    public bool ShowFancyText { get; set; }

    public double CoolLevel { get; } = 42.1337;
}
