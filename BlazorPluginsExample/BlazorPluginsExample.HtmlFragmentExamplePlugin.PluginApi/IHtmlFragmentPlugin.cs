namespace BlazorPluginsExample.HtmlFragmentExamplePlugin.PluginApi;

public interface IHtmlFragmentPlugin
{
    Task<string> GetFragmentContentAsync();
}