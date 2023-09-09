namespace BlazorPluginsExample.PluginApi;

public interface IPluginApi
{
    Task<string> GetDataAsync();
}