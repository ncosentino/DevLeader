namespace BlazorPluginsExample.NavigationPluginApi;

public interface INavigationPlugin
{
    Task<IReadOnlyList<NavigationItem>> GetNavigationItemsAsync();
}