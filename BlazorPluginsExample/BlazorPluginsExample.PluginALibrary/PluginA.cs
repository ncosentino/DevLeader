using BlazorPluginsExample.PluginApi;

namespace BlazorPluginsExample.PluginALibrary;

public sealed class PluginA : IPluginApi
{
    public Task<string> GetDataAsync() => 
        Task.FromResult("Data from plugin A!");
}
