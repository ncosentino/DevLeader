using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFusionCache();

var app = builder.Build();
app.UseHttpsRedirection();

int counter = 0;
app.MapGet("/weatherforecast", async (IFusionCache cache) =>
{
    var forecast = await cache.GetOrSetAsync(
        "cacheKey",
        async ct => await CreateForecastAsync(counter++),
        new FusionCacheEntryOptions()
        {
            IsFailSafeEnabled = true,
        });

    return forecast;
});

app.MapGet("/expire", async (IFusionCache cache) =>
{
    await cache.ExpireAsync("cacheKey");
});
app.MapGet("/remove", async (IFusionCache cache) =>
{
    await cache.RemoveAsync("cacheKey");
});

app.Run();

static async Task<IReadOnlyCollection<WeatherForecast>> CreateForecastAsync(int counter)
{
    if (counter == 2)
    {
        throw new InvalidOperationException("Oh no!!");
    }

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

    Console.WriteLine($"Forecast created: {counter}");
    return forecast;
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
