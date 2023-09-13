namespace BlazorPluginsExample.PluginApi.Shared;

/// <summary>
/// FIXME: This class should be moved to a shared 
/// library and not actually the plugin left-over 
/// plugin API library.
/// </summary>
public sealed class GenericPluginProvider<TPluginApi>
{
    private readonly Lazy<IReadOnlyList<TPluginApi>> _lazyPlugins;

    public GenericPluginProvider(
        Lazy<IEnumerable<TPluginApi>> lazyPlugins)
    {
        _lazyPlugins = new(lazyPlugins.Value.ToArray());
    }

    public IReadOnlyList<TPluginApi> GetPlugins()
    {
        return _lazyPlugins.Value;
    }
}
