using System.Reflection;

using Autofac;

using BlazorPluginsExample.PluginApi;

namespace BlazorPluginsExample;

public sealed class AutofacModule : global::Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<PluginProvider>()
            .SingleInstance();
        builder
            .RegisterType<GenericPluginProvider<IPluginApi2>>()
            .SingleInstance();
        builder
            .RegisterType<GenericPluginProvider<IPluginApi3>>()
            .SingleInstance();

        foreach (var file in Directory.EnumerateFiles(
            AppDomain.CurrentDomain.BaseDirectory,
            "*plugin*.dll",
            SearchOption.TopDirectoryOnly))
        {
            var assembly = Assembly.LoadFrom(file);
            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => 
                    typeof(IPluginApi3).IsAssignableFrom(t)
                    && !t.IsAbstract
                    && t.IsClass)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}