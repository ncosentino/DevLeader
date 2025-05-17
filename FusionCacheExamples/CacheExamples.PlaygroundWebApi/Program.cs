using Microsoft.AspNetCore.Mvc;

using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddFusionCache();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async ([FromQuery] int dayOfWeek, IFusionCache cache) =>
{
    var forecast = await cache.GetOrSetAsync(
        dayOfWeek.ToString(),
        async ct => await CreateForecastAsync(dayOfWeek),
        new FusionCacheEntryOptions()
        {
            //Duration = TimeSpan.FromSeconds(5)
        });

    return forecast;
});

Task.Run(async () =>
{
    await Task.Delay(TimeSpan.FromSeconds(5));

    var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();

    var tasks = Enumerable.Range(0, 1000).Select(async _ =>
    {
        var client = httpClientFactory.CreateClient();
        await client.GetAsync("https://localhost:7236/weatherforecast?dayOfWeek=1");
    }).ToArray();
    await Task.WhenAll(tasks);
});

app.Run();

async Task<IReadOnlyList<WeatherForecast>> CreateForecastAsync(int dayOfWeek)
{
    await Task.Delay(TimeSpan.FromSeconds(1));

    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}