using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SettingsOptions>(
    builder.Configuration.GetSection("Settings"));

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/hello", (IOptionsMonitor<SettingsOptions> settings) =>
{
    return new
    {
        String = settings.CurrentValue.StringProperty,
        Integer = settings.CurrentValue.IntegerProperty,
    };
});

app.Run();

//public sealed record SettingsOptions(
//    string StringProperty,
//    string IntegerProperty);

public class SettingsOptions
{
    public string? StringProperty { get; init; }
    public int? IntegerProperty { get; init; }
}