using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(new RedisConfiguration()
{
    ConnectionString = builder
        .Configuration
        .GetConnectionString("redis")
        ?? throw new InvalidOperationException("Missing redis connection string!")
});

var app = builder.Build();
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing",
    "Bracing", 
    "Chilly",
    "Cool", 
    "Mild",
    "Warm", 
    "Balmy",
    "Hot", 
    "Sweltering", 
    "Scorching"
};
app.MapGet("/weatherforecast", async (IRedisDatabase redisDatabase) =>
{
    var cached = await redisDatabase.GetAsync<WeatherForecast[]>("weather");
    if (cached is not null)
    {
        Console.WriteLine("Got cached forecast!");
        return Results.Ok(cached);
    }

    Console.WriteLine("Creating a new forecast!");
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    await redisDatabase.AddAsync("weather", forecast);
    return Results.Ok(forecast);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
