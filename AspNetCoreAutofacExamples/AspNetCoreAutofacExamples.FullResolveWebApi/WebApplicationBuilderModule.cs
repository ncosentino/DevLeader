using Autofac;
using Autofac.Extensions.DependencyInjection;

using Plugin.SDK;

internal sealed class WebApplicationBuilderModule : global::Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(ctx =>
            {
                var builder = WebApplication.CreateBuilder(Environment.GetCommandLineArgs());
                return builder;
            })
            .SingleInstance();
        builder
            .Register(ctx =>
            {
                var config = ctx.Resolve<WebApplicationBuilder>().Configuration;
                return config;
            })
            .As<IConfiguration>()
            .SingleInstance();

        WebApplication? cachedWebApplication = null;
        builder
            .Register(ctx =>
            {
                if (cachedWebApplication is not null)
                {
                    return cachedWebApplication;
                }

                var webApplicationBuilder = ctx.Resolve<WebApplicationBuilder>();
                ctx.Resolve<IReadOnlyList<PreApplicationBuildMarker>>();

                var serviceProviderFactory = new AutofacServiceProviderFactory(containerBuilder =>
                {
                    foreach (var registration in ctx.ComponentRegistry.Registrations)
                    {
                        containerBuilder.ComponentRegistryBuilder.Register(registration);
                    }

                    containerBuilder
                        .RegisterInstance(webApplicationBuilder)
                        .SingleInstance();
                });
                webApplicationBuilder.Host.UseServiceProviderFactory(serviceProviderFactory);

                cachedWebApplication = webApplicationBuilder.Build();
                return cachedWebApplication;
            })
            .SingleInstance();
        builder
            .Register(ctx =>
            {
                var app = ctx.Resolve<WebApplication>();
                app.UseHttpsRedirection();
                return new PreApplicationConfiguredMarker();
            })
            .SingleInstance();
        builder
            .RegisterType<ConfiguredWebApplication>()
            .SingleInstance();
    }
}
