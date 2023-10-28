using Autofac;

internal sealed class WebApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(ctx => WebApplication
                .CreateBuilder(Environment.GetCommandLineArgs()))
            .SingleInstance();
        builder
            .Register(ctx =>
            {
                var webApplicationBuilder = ctx.Resolve<WebApplicationBuilder>();
                var webApplication = webApplicationBuilder.Build();
                return webApplication;
            })
            .SingleInstance();
        builder
            .Register(ctx =>
            {
                // we use this to ensure any route dependencies that need to
                // be registered have had a chance to do so!
                ctx.Resolve<IEnumerable<RouteRegistrationDependencyMarker>>();

                var webApplication = ctx.Resolve<WebApplication>();
                webApplication.UseHttpsRedirection();
                ConfiguredWebApplication configuredWebApplication = new(webApplication);
                return configuredWebApplication;
            })
            .SingleInstance();
    }
}
