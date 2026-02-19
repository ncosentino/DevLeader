using Microsoft.Extensions.Logging;

using Serilog;

using ILogger = Microsoft.Extensions.Logging.ILogger;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}]{NewLine}  {Properties}{NewLine}  {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

using var factory = LoggerFactory.Create(builder => builder.AddSerilog());
var logger = factory.CreateLogger<Program>();

// Simulate a top-level operation (like a web request)
int userId = 1001;
var correlationId = Guid.NewGuid();
OldWay_DoOperation(logger, userId, correlationId);

Console.WriteLine("--------");

using IDisposable scope = logger.BeginScope(new Dictionary<string, object?>
{
    ["UserId"] = userId,
    ["CorrelationId"] = correlationId
});
NewWay_DoOperation(logger);

static void NewWay_DoOperation(ILogger logger)
{
    logger.LogInformation("Starting operation for user.");
    NewWay_ProcessData(logger);
}

static void NewWay_ProcessData(ILogger logger)
{
    logger.LogInformation("Processing data for user.");
    NewWay_SaveResults(logger);
}

static void NewWay_SaveResults(ILogger logger)
{
    logger.LogInformation("Saving results for user.");
}

static void OldWay_DoOperation(
    ILogger logger,
    int userId, 
    Guid correlationId)
{
    logger.LogInformation(
        "Starting operation for user {UserId} with correlation {CorrelationId}", 
        userId,
        correlationId);
    OldWay_ProcessData(logger, userId, correlationId);
}

static void OldWay_ProcessData(
    ILogger logger,
    int userId, 
    Guid correlationId)
{
    logger.LogInformation(
        "Processing data for user {UserId} with correlation {CorrelationId}",
        userId,
        correlationId);
    OldWay_SaveResults(logger, userId, correlationId);
}

static void OldWay_SaveResults(
    ILogger logger,
    int userId,
    Guid correlationId)
{
    logger.LogInformation(
        "Saving results for user {UserId} with correlation {CorrelationId}", 
        userId, 
        correlationId);
}