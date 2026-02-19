using Microsoft.Extensions.Logging;

using Serilog;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext() // Needed for BeginScope
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}]{NewLine}  {Properties}{NewLine}  {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// Create Microsoft.Extensions.Logging ILogger
using var factory = LoggerFactory.Create(builder =>
{
    builder.AddSerilog(dispose: true);
});
Microsoft.Extensions.Logging.ILogger logger = factory.CreateLogger<Program>();

logger.LogInformation("Starting application...");

// Logging without a scope
logger.LogInformation("Processing started");

int userId = 42;
Guid correlationId = Guid.NewGuid();
//logger.LogInformation(
//    "User {UserId} initiated a request with CorrelationId {CorrelationId}",
//    userId, 
//    correlationId);
//logger.LogWarning(
//    "Potential issue detected for User {UserId}", 
//    userId);

using IDisposable scope = logger.BeginScope(new Dictionary<string, object>
{
    ["UserId"] = userId,
    ["CorrelationId"] = correlationId
});

logger.LogInformation("User initiated a request.");
logger.LogWarning("Potential issue detected for user.");
logger.LogInformation("Finished processing");