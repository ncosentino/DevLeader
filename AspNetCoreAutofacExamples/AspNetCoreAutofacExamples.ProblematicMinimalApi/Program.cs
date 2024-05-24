using Autofac;

using Microsoft.AspNetCore.Mvc;

// personal opinion:
// I absolutely love having the entry point of my
// applications being essentially:
// - make my dependencies
// - give me the primary dependency
// - use it
// - ... nothing else :)
ContainerBuilder containerBuilder = new();
containerBuilder.RegisterModule<MyModule>();

using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();
var app = scope.Resolve<WebApplication>();
app.Run();

// ---- The few lines above are the only thing that should be in Program.cs ----
// -----------------------------------------------------------------------------

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal sealed class MyModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<DependencyA>().SingleInstance();
        containerBuilder.RegisterType<DependencyB>().SingleInstance();
        containerBuilder.RegisterType<DependencyC>().SingleInstance();
        containerBuilder
            .Register(ctx =>
            {
                var builder = WebApplication.CreateBuilder(Environment.GetCommandLineArgs());
                return builder;
            })
            .SingleInstance();
        containerBuilder
            .Register(ctx => ctx.Resolve<WebApplicationBuilder>().Configuration)
            .As<IConfiguration>()
            .SingleInstance();
        containerBuilder
            .Register(ctx =>
            {
                var builder = ctx.Resolve<WebApplicationBuilder>();

                var app = builder.Build();
                app.UseHttpsRedirection();

                /*
                // FIXME: the problem is that the Autofac ContainerBuilder
                // was used to put all of these pieces together,
                // but we never told the web stack to use Autofac as the
                // service provider.
                // this means that the minimal API will never be able to
                // find services off the container. we would need to resolve
                // them BEFORE the API is called, like in this registration
                // method itself, from the context that is passed in.
                //DependencyA dependencyA = ctx.Resolve<DependencyA>();

                // FIXME: But... What happens if something wants to take a
                // dependency on the WebApplication instance itself? Once the
                // web application has been built, there's no more adding
                // dependencies to it!
                */

                var summaries = new[]
                {
                    "Freezing", "Bracing", "Chilly", "Cool", 
                    "Mild", "Warm", "Balmy", "Hot", "Sweltering",
                    "Scorching"
                };

                app.MapGet(
                    "/weatherforecast",
                    (
                        [FromServices] DependencyA dependencyA
                      , [FromServices] DependencyB dependencyB
                      , [FromServices] DependencyC dependencyC
                    ) =>
                    {
                        var forecast = Enumerable
                            .Range(1, 5)
                            .Select(index => new WeatherForecast
                            (
                                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                                Random.Shared.Next(-20, 55),
                                summaries[Random.Shared.Next(summaries.Length)]
                            ))
                            .ToArray();
                        return forecast;
                    });

                
                var weatherForecastRoutes = ctx.Resolve<WeatherForecastRoutes>();
                //app.MapGet("/weatherforecast2", weatherForecastRoutes.Forecast);                
                foreach (var registrar in ctx.Resolve<IEnumerable<IRegisterRoutes>>())
                {
                    registrar.RegisterRoutes(app);
                }

                return app;
            })
            .SingleInstance();

        
        
        containerBuilder
            .RegisterType<WeatherForecastRoutes>()
            .SingleInstance();     
        containerBuilder
            .RegisterType<WeatherForecastRouteRegistrar>()
            .AsImplementedInterfaces()
            .SingleInstance();
      
    }
}

internal sealed class DependencyA(
    WebApplicationBuilder _webApplicationBuilder);

internal sealed class DependencyB(
    WebApplication _webApplication);

internal sealed class DependencyC(
    IConfiguration _configuration);


internal sealed class WeatherForecastRoutes(
    DependencyA _dependencyA
//, DependencyB _dependencyB // FIXME: still can't depend on this because we can't get the WebApplication
  , DependencyC _dependencyC)
{
    private static readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecast[] Forecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}


internal interface IRegisterRoutes
{
    void RegisterRoutes(WebApplication app);
}

internal sealed class WeatherForecastRouteRegistrar(
    WeatherForecastRoutes _weatherForecastRoutes) :
    IRegisterRoutes
{
    public void RegisterRoutes(WebApplication app)
    {
        app.MapGet("/weatherforecast2", _weatherForecastRoutes.Forecast);
    }
}