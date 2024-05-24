using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder
        .RegisterInstance(builder)
        .SingleInstance();
    containerBuilder
        .Register(ctx => ctx.Resolve<WebApplicationBuilder>().Configuration)
        .SingleInstance();
    /*
    // FIXME: we can't do this because the WebApplicationBuilder
    // the WebApplicationBuilder is responsible for building the
    // WebApplication, so we can't do that manually just to add
    // it into the container
    containerBuilder
        .Register(ctx =>
        {
            var app = ctx.Resolve<WebApplicationBuilder>().Build();
            app.UseHttpsRedirection();
            return app;
        })
        .SingleInstance();
    */
    containerBuilder.RegisterType<DependencyA>().SingleInstance();
    containerBuilder.RegisterType<DependencyB>().SingleInstance();
    containerBuilder.RegisterType<DependencyC>().SingleInstance();
    
    containerBuilder
        .RegisterBuildCallback(ctx =>
        {
            // FIXME: this was never registered
            var app = ctx.Resolve<WebApplication>();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet(
                "/weatherforecast",
                (
                    [FromServices] DependencyA dependencyA // this will work
                  , [FromServices] DependencyB dependencyB // FIXME: this will fail!!
                  , [FromServices] DependencyC dependencyC // this will work
                ) =>
                {
                    var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                        .ToArray();
                    return forecast;
                });
        });
}));

/*
// FIXME: we can't get the WebApplication into the
// Autofac container, because it's already been built.
// this means if we have anything that wants to take a
// dependency on the WebApplication instance itself, we
// can't resolve it from the container.
*/
var app = builder.Build();
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet(
    "/weatherforecast",
    (
        [FromServices] DependencyA dependencyA // will this work?
      , [FromServices] DependencyC dependencyC // will this work?
      , [FromServices] DependencyB dependencyB // will this work?
    ) =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    });

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal sealed class DependencyA(
    WebApplicationBuilder _webApplicationBuilder);

internal sealed class DependencyB(
    WebApplication _webApplication);

internal sealed class DependencyC(
    IConfiguration _configuration);