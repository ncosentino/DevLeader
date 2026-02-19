using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog from appsettings.json (and environment-specific appsettings)
// Keep Serilog configuration in JSON, not in code.
builder.Host.UseSerilog((ctx, services, loggerConfig) =>
    loggerConfig
        .ReadFrom.Configuration(ctx.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Serilog request logging middleware (structured request logs)
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
