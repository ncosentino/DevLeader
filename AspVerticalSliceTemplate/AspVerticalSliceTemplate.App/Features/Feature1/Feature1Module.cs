using Autofac;

namespace AspVerticalSliceTemplate.App.Features.Feature1;

internal sealed class Feature1Module : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(ctx =>
            {
                var app = ctx.Resolve<WebApplication>();

                app.MapGet("hello", () => "Hello World!");

                return new RouteRegistrationDependencyMarker();
            })
            .SingleInstance();
    }
}