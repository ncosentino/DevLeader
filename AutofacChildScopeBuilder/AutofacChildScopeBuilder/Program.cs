// Assume this is your main application's lifetime scope
using Autofac;

using System.Reflection;

ILifetimeScope _scope = null;

// NOTE: this does not have any safety checks that you
// may want to consider when loading assemblies
var assembly = Assembly.LoadFrom("some path");
var pluginType = typeof(IPlugin);
var plugins = assembly.GetTypes()
    .Where(p => pluginType.IsAssignableFrom(p) && !p.IsInterface)
    .ToList();

var childScope = _scope.BeginLifetimeScope(builder =>
{
    foreach (var plugin in plugins)
    {
        builder.RegisterType(plugin).As<IPlugin>();
    }
});

foreach (var plugin in plugins)
{
    var instance = childScope.Resolve<IPlugin>();
    // Use the plugin instance as needed
} 


public interface IPlugin
{

}