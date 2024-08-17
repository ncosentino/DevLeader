
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows;
using System.Diagnostics;

public static class StitchingDependencies
{
    public static StitcherFactory CreateStitcherFactory()
    {
        IViewStitcher CreateGridViewStitcher(object parent, object child)
        {
            return new WpfGridSticher((Grid)parent, (UIElement)child);
        };

        List<StitcherRegistration> stitcherRegistrations = new()
        {
            new StitcherRegistration(
                (parent, child) => parent is ContentControl && child is UIElement,
                (parent, child) => new SimpleContentStitcher(
                (ContentControl)parent,
                (UIElement)child)),
            new StitcherRegistration(
                (parent, child) => parent is Grid && child is UIElement,
                (parent, child) => CreateGridViewStitcher(parent, child)),
            new StitcherRegistration(
                (parent, child) => parent is IAddChild && child is UIElement,
                (parent, child) => new FallbackWpfGridStitcher(
                    (IAddChild)parent,
                    (UIElement)child,
                    CreateGridViewStitcher)),
        };
        StitcherFactory stitcherFactory = new(stitcherRegistrations);

        return stitcherFactory;
    }
}

public interface IViewStitcher
{
}

public delegate bool CanMatchDelegate(
    object parent,
    object child);

public delegate IViewStitcher CreateStitcherDelegate(
    object parent,
    object child);

public sealed record StitcherRegistration(
    CanMatchDelegate CanMatchCallback,
    CreateStitcherDelegate CreateStitcherCallback);

public sealed class StitcherFactory
{
    private readonly IReadOnlyList<StitcherRegistration> _stitcherRegistrations;

    public StitcherFactory(IReadOnlyList<StitcherRegistration> stitcherRegistrations)
    {
        _stitcherRegistrations = stitcherRegistrations;
    }

    public TViewStitcher Create<TViewStitcher>(object parent, object child)
        where TViewStitcher : IViewStitcher
    {
        var stitcher = _stitcherRegistrations
            .FirstOrDefault(x => x.CanMatchCallback(parent, child))
            ?.CreateStitcherCallback(parent, child)
            ?? throw new NotSupportedException(
                $"No stitcher found for parent '{parent}' " +
                $"and child '{child}'");

        if (stitcher is not TViewStitcher castedStitcher)
        {
            throw new InvalidCastException(
                $"Stitcher '{stitcher}' is not of type " +
                $"'{typeof(TViewStitcher)}'");
        }

        return castedStitcher;
    }
}

public interface ISimpleStitcher : IViewStitcher
{
    void Stitch();
}

public interface IGridStitcher : IViewStitcher
{
    void Stitch(int row, int column);
}

public sealed class SimpleContentStitcher : ISimpleStitcher
{
    private readonly ContentControl _parent;
    private readonly UIElement _child;

    public SimpleContentStitcher(ContentControl parent, UIElement child)
    {
        _parent = parent;
        _child = child;
    }

    public void Stitch()
    {
        _parent.Content = _child;
    }
}

public sealed class WpfGridSticher : IGridStitcher
{
    private readonly Grid _parent;
    private readonly UIElement _child;

    public WpfGridSticher(Grid parent, UIElement child)
    {
        _parent = parent;
        _child = child;
    }

    public void Stitch(int row, int column)
    {
        _parent.Children.Add(_child);
        Grid.SetRow(_child, row);
        Grid.SetColumn(_child, column);
    }
}

public sealed class FallbackWpfGridStitcher : IGridStitcher
{
    private readonly IAddChild _parent;
    private readonly UIElement _child;
    private readonly CreateStitcherDelegate _createStitcherCallback;

    public FallbackWpfGridStitcher(
        IAddChild parent,
        UIElement child,
        CreateStitcherDelegate createStitcherCallback)
    {
        if (parent is Grid)
        {
            throw new NotSupportedException(
                $"Parent '{parent}' should NOT be a '{typeof(Grid)}'!");
        }

        _parent = parent;
        _child = child;
        _createStitcherCallback = createStitcherCallback;
    }

    public void Stitch(int row, int column)
    {
        // NOTE: we are not giving control to the caller about
        // how the grid gets put into the parent control.
        Grid grid = new();
        grid.HorizontalAlignment = HorizontalAlignment.Stretch;
        grid.VerticalAlignment = VerticalAlignment.Stretch;
        _parent.AddChild(grid);

        var gridSticher = (IGridStitcher)_createStitcherCallback.Invoke(
            grid,
            _child);
        gridSticher.Stitch(row, column);
    }
}