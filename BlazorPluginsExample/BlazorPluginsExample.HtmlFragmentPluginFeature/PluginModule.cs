using Autofac;

using BlazorPluginsExample.HtmlFragmentExamplePlugin.PluginApi;
using BlazorPluginsExample.PluginApi.Shared;

namespace BlazorPluginsExample.HtmlFragmentPluginLibrary;

public sealed class PluginModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<GenericPluginProvider<IHtmlFragmentPlugin>>()
            .SingleInstance();
        builder
            .RegisterType<LateLoadPluginProvider<IHtmlFragmentPlugin>>()
            .SingleInstance();
    }
}
