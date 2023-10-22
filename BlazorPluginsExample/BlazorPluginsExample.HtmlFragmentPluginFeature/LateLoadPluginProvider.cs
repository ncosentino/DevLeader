using System.Reflection;

using Autofac;

using BlazorPluginsExample.PluginApi.Shared;

namespace BlazorPluginsExample.HtmlFragmentPluginLibrary;

public sealed class LateLoadPluginProvider<TPluginApi>
{
    private readonly GenericPluginProvider<TPluginApi> _genericPluginProvider;
    private readonly List<TPluginApi> _plugins;

    public LateLoadPluginProvider(
        GenericPluginProvider<TPluginApi> genericPluginProvider)
    {
        _genericPluginProvider = genericPluginProvider;
        _plugins = new();

        FileSystemWatcher watcher = new(
            AppDomain.CurrentDomain.BaseDirectory,
            "*plugin*.dll");
        watcher.EnableRaisingEvents = true;
        watcher.Created += Watcher_Created;
    }

    public event EventHandler<EventArgs>? PluginsChanged;

    public IReadOnlyList<TPluginApi> GetPlugins()
    {
        var combinedPlugins = _genericPluginProvider
            .GetPlugins()
            .ToList();
        combinedPlugins.AddRange(_plugins);
        return combinedPlugins;
    }

    private void Watcher_Created(
        object sender,
        FileSystemEventArgs e)
    {
        // TODO: can we provide dependencies from the core app?
        ContainerBuilder builder = new();

        var assembly = Assembly.LoadFrom(e.FullPath);
        builder
            .RegisterAssemblyTypes(assembly)
            .Where(t =>
                typeof(TPluginApi).IsAssignableFrom(t)
                && !t.IsAbstract
                && t.IsClass)
            .AsImplementedInterfaces()
            .SingleInstance();

        // TODO: how do we want to handle the lifetime here? if
        // we call Dispose() too early, any delayed service
        // resolution will blow up
        var container = builder.Build();
        var scope = container.BeginLifetimeScope();
        var plugins = scope.Resolve<IEnumerable<TPluginApi>>();
        _plugins.AddRange(plugins);

        PluginsChanged?.Invoke(this, EventArgs.Empty);
    }
}
