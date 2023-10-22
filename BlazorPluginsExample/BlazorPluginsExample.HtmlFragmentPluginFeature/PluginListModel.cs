using System.ComponentModel;

namespace BlazorPluginsExample.HtmlFragmentPluginLibrary;

public sealed class PluginListModel<TPluginApi> : INotifyPropertyChanged
{
    private readonly LateLoadPluginProvider<TPluginApi> _pluginProvider;

    public PluginListModel(LateLoadPluginProvider<TPluginApi> pluginProvider)
    {
        _pluginProvider = pluginProvider;
        _pluginProvider.PluginsChanged += PluginProvider_PluginsChanged;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public IReadOnlyList<TPluginApi> Plugins =>
        _pluginProvider.GetPlugins();

    private void PluginProvider_PluginsChanged(object? sender, EventArgs e)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Plugins)));
    }
}
