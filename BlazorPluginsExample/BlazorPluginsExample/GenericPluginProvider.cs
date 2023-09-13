namespace BlazorPluginsExample;

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
