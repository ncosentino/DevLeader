using AiCodeReviewBot.Agents;
using AiCodeReviewBot.Configuration;
using AiCodeReviewBot.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

// Parse command-line arguments
var filePath = string.Empty;
var outputPath = string.Empty;

for (int i = 0; i < args.Length; i++)
{
    if ((args[i] == "--path" || args[i] == "-p") && i + 1 < args.Length)
        filePath = args[++i];
    else if ((args[i] == "--output" || args[i] == "-o") && i + 1 < args.Length)
        outputPath = args[++i];
    else if (!args[i].StartsWith('-'))
        filePath = args[i];
}

if (string.IsNullOrEmpty(filePath))
{
    Console.Error.WriteLine("Usage: ai-code-review-bot --path <file-or-folder> [--output <report.md>]");
    Console.Error.WriteLine("  --path, -p   Path to a .cs file or folder containing .cs files");
    Console.Error.WriteLine("  --output, -o Output path for the markdown report (default: stdout)");
    return 1;
}

if (!File.Exists(filePath) && !Directory.Exists(filePath))
{
    Console.Error.WriteLine($"Error: Path not found: {filePath}");
    return 1;
}

// Load configuration
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables(prefix: "AICODEBOT_")
    .Build();

var providerConfig = config.GetSection("AIProvider").Get<AIProviderConfig>()
    ?? throw new InvalidOperationException("AIProvider section is missing from configuration.");

if (string.IsNullOrEmpty(providerConfig.ApiKey))
{
    Console.Error.WriteLine("Error: AIProvider:ApiKey is not configured.");
    Console.Error.WriteLine("  Set it in appsettings.Development.json or via environment variable AICODEBOT_AIProvider__ApiKey");
    return 1;
}

// Build Kernel with configured provider
var builder = Kernel.CreateBuilder();

if (providerConfig.Type.Equals("azureopenai", StringComparison.OrdinalIgnoreCase))
{
    if (string.IsNullOrEmpty(providerConfig.Endpoint))
        throw new InvalidOperationException("AIProvider:Endpoint is required when Type is 'azureopenai'.");

    builder.AddAzureOpenAIChatCompletion(
        deploymentName: providerConfig.ModelId,
        endpoint: providerConfig.Endpoint,
        apiKey: providerConfig.ApiKey);
}
else
{
    builder.AddOpenAIChatCompletion(
        modelId: providerConfig.ModelId,
        apiKey: providerConfig.ApiKey);
}

// Register review plugins
builder.Plugins.AddFromType<BugDetectionPlugin>();
builder.Plugins.AddFromType<SecurityPlugin>();
builder.Plugins.AddFromType<PerformancePlugin>();
builder.Plugins.AddFromType<StylePlugin>();

var kernel = builder.Build();

// Discover C# files
string[] files;

if (File.Exists(filePath))
{
    files = [filePath];
}
else
{
    files = Directory.GetFiles(filePath, "*.cs", SearchOption.AllDirectories)
        .Where(f =>
            !f.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") &&
            !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
        .OrderBy(f => f)
        .ToArray();
}

if (files.Length == 0)
{
    Console.Error.WriteLine("No .cs files found at the specified path.");
    return 1;
}

Console.WriteLine($"AI Code Review Bot -- {files.Length} file(s) to review");
Console.WriteLine($"Provider: {providerConfig.Type} | Model: {providerConfig.ModelId}");
Console.WriteLine(new string('-', 60));

// Run reviews
var orchestrator = new ReviewOrchestrator(kernel);
var results = new List<string>();
var failedFiles = new List<string>();

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
    Console.Error.WriteLine("\nCancelled.");
};

foreach (var file in files)
{
    var displayName = Path.GetRelativePath(Directory.GetCurrentDirectory(), file);
    Console.Write($"  Reviewing {displayName}... ");

    var code = await File.ReadAllTextAsync(file, cts.Token);

    // Warn on very large files
    if (code.Length > 50_000)
    {
        Console.WriteLine("⚠ File is large (>50K chars) -- review may be truncated by the model.");
        code = code[..50_000];
    }

    var result = await orchestrator.ReviewCodeAsync(code, Path.GetFileName(file), cts.Token);

    if (result.Success)
    {
        Console.WriteLine("✓");
        results.Add($"## Review: `{Path.GetFileName(file)}`\n\n{result.Review}");
    }
    else
    {
        Console.WriteLine($"✗ ({result.ErrorMessage})");
        failedFiles.Add($"{Path.GetFileName(file)}: {result.ErrorMessage}");
        results.Add($"## Review: `{Path.GetFileName(file)}`\n\n⚠ Review failed: {result.ErrorMessage}");
    }
}

// Build final report
var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
var failureSummary = failedFiles.Count > 0
    ? $"\n\n> ⚠ **{failedFiles.Count} file(s) failed:** {string.Join(", ", failedFiles)}"
    : string.Empty;

var report = $"""
    # AI Code Review Report

    **Generated:** {timestamp}
    **Files reviewed:** {files.Length} ({files.Length - failedFiles.Count} succeeded, {failedFiles.Count} failed)
    **Provider:** {providerConfig.Type} / {providerConfig.ModelId}{failureSummary}

    ---

    {string.Join("\n\n---\n\n", results)}
    """;

if (!string.IsNullOrEmpty(outputPath))
{
    await File.WriteAllTextAsync(outputPath, report, cts.Token);
    Console.WriteLine($"\nReport saved to: {outputPath}");
}
else
{
    Console.WriteLine();
    Console.WriteLine(report);
}

return failedFiles.Count > 0 ? 2 : 0;
