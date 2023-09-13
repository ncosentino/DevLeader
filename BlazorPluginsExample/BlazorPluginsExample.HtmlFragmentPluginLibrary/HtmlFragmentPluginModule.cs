using Autofac;

namespace BlazorPluginsExample.HtmlFragmentPluginLibrary;

public sealed class HtmlFragmentPluginModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<HtmlFragmentPlugin>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}