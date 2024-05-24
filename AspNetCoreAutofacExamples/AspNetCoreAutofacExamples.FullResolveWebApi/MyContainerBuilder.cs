using Autofac;

using System.Reflection;

internal sealed class MyContainerBuilder
{
    public IContainer Build()
    {
        ContainerBuilder containerBuilder = new();

        // TODO: do some assembly scanning if needed
        //var assembly = Assembly.GetExecutingAssembly();
        //containerBuilder.RegisterAssemblyModules(assembly);

        // write assembly scanning code to load modules into autofac
        // from all of the DLLs in the running directory
        var assemblies = Directory
            .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(Assembly.LoadFrom)
            .ToArray();
        containerBuilder.RegisterAssemblyModules(assemblies);

        var container = containerBuilder.Build();
        return container;
    }
}