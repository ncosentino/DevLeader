using BlazorPluginsExample.NavigationPluginApi;

namespace BlazorPluginsExample.HtmlFragmentPluginLibrary;

public sealed class HtmlFragmentNavigationPlugin : INavigationPlugin
{
    private readonly IReadOnlyList<NavigationItem> _navigationItems;

    public HtmlFragmentNavigationPlugin()
    {
        _navigationItems = new[]
        {
            new NavigationItem(
                "Html Fragments",
                "htmlfragmentspage"),
        };
    }

    public Task<IReadOnlyList<NavigationItem>> GetNavigationItemsAsync()
        => Task.FromResult(_navigationItems);
}