using Autofac;

using System.Reflection;

ContainerBuilder containerBuilder = new();
// TODO: use something like this for assembly scanning
//DependencyScanner.ScanAndRegisterDependencies(containerBuilder);
containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

foreach (var pluginType in Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(x => typeof(IRepository).IsAssignableFrom(x)))
{
    containerBuilder
        .RegisterType(pluginType)
        .As<IRepository>()
        .SingleInstance();
}

using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();

var plugins = scope
    .Resolve<IEnumerable<IRepository>>()
    .ToArray();
foreach (var plugin in plugins)
{
    Console.WriteLine(plugin.GetType().Name);
}

public sealed class DependencyForOurPlugin
{
    public IEnumerable<MyObject> GetAllObjects() => new[]
    {
        new MyObject("From our dependency!"),
    };
}

public sealed class OurRepositoryPlugin(
    DependencyForOurPlugin _ourDependency) : IRepository
{
    public IEnumerable<MyObject> GetAllObjects()
        => _ourDependency.GetAllObjects();
}

public static class DependencyScanner
{
    public static void ScanAndRegisterDependencies(
        ContainerBuilder containerBuilder)
    {
        var pluginAssemblies = Directory
            .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(Assembly.LoadFile)
            .ToArray();
        containerBuilder
            .RegisterAssemblyTypes(pluginAssemblies)
            .AssignableTo<IRepository>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}

public sealed class OurModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<DependencyForOurPlugin>()
            .SingleInstance();
    }
}