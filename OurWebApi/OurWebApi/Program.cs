using Dapper;

using DbUp;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/weatherforecast",
async 
(
    [FromQuery] string minimumDateTimeUtc,
    CancellationToken cancellationToken
) =>
{
    var connectionString = app.Configuration
        .GetConnectionString("DefaultConnection");
    using var sqliteConnection = new SqliteConnection(connectionString);
    await sqliteConnection.OpenAsync(cancellationToken);

    WeatherForecast[] result = (await sqliteConnection
        .QueryAsync<WeatherForecast>(new CommandDefinition(
            """
            SELECT
                Id,
                DateTimeUtc,
                TemperatureC
            FROM 
                WeatherForecast
            WHERE
                DateTimeUtc >= @minimum_date_time_utc
            ;
            """,
            new
            {
                minimum_date_time_utc = minimumDateTimeUtc
            },
            cancellationToken: cancellationToken)))
        .ToArray();

    return Results.Ok(result.Select(x => new
    {
        DateTimeUtc = x.DateTimeUtc,
        TemperatureC = x.TemperatureC
    }));
});

var connectionStrings = app
    .Configuration
    .GetSection("ConnectionStrings")
    .Get<ConnectionStrings>();
var upgrader = DeployChanges.To
    .SQLiteDatabase(connectionStrings.DefaultConnection)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .Build();

var result = upgrader.PerformUpgrade();

app.Run();

public record ConnectionStrings(
    string DefaultConnection);

public record WeatherForecast(
    long Id,
    string DateTimeUtc,
    double TemperatureC);
