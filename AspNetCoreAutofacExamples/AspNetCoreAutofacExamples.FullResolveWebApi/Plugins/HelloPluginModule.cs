using Autofac;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Plugin.SDK;

namespace Plugins;

internal sealed class HelloPluginModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(ctx =>
            {
                var app = ctx.Resolve<WebApplication>();
                app.MapGet("/hello", () => Results.Ok("Hey there!"));
                return new PreApplicationConfiguredMarker();
            })
            .SingleInstance();
    }
}
