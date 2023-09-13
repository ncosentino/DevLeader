using Autofac;

namespace BlazorPluginsExample.HtmlFragmentPluginLibrary;

public sealed class HtmlFragmentPluginModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // NOTE: this is leftover from an earlier example
        // in the tutorial where we do not use reflection
        // to auto-discover and register from the parent
        builder
            .RegisterType<HtmlFragmentPlugin>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}