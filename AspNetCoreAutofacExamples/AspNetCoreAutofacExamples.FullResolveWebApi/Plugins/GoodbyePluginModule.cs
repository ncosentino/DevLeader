using Autofac;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Plugin.SDK;

namespace Plugins;

internal sealed class GoodbyePluginModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(ctx =>
            {
                var app = ctx.Resolve<WebApplication>();
                app.MapGet("/goodbye", () => Results.Ok("See ya later!"));
                return new PreApplicationConfiguredMarker();
            })
            .SingleInstance();
    }
}