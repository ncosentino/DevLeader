using Microsoft.AspNetCore.Components;

namespace BlazorPluginsExample.PluginApi;

public interface IPluginApi
{
    Task<string> GetDataAsync();
}

public interface IPluginApi2
{
    Task<string> GetFragmentContentAsync();
}

public interface IPluginApi3
{
    Task<RenderFragment> GetRenderFragmentAsync();
}