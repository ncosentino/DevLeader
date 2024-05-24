using Autofac;

internal sealed class FullResolveWebApi
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        var containerBuilder = new MyContainerBuilder();
        using var container = containerBuilder.Build();
        using var scope = container.BeginLifetimeScope();
        
        var app = scope
            .Resolve<ConfiguredWebApplication>()
            .WebApplication;
        await app.RunAsync(cancellationToken).ConfigureAwait(false);
    }
}