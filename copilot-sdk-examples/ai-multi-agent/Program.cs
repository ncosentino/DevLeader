using AiMultiAgent.Agents;
using AiMultiAgent.Configuration;
using GitHub.Copilot.SDK;
using Microsoft.Extensions.Configuration;

// ── Configuration ─────────────────────────────────────────────────────────────
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables("MULTI_AGENT_")
    .Build();

var config = configuration.GetSection(MultiAgentConfig.SectionName).Get<MultiAgentConfig>()
    ?? new MultiAgentConfig();

// ── Resolve target file ───────────────────────────────────────────────────────
var targetFile = args.Length > 0 ? args[0] : null;
if (string.IsNullOrWhiteSpace(targetFile))
{
    Console.Write("Enter path to the C# file to analyze: ");
    targetFile = Console.ReadLine()?.Trim();
}

if (string.IsNullOrWhiteSpace(targetFile) || !File.Exists(targetFile))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[Error] File not found: {targetFile}");
    Console.ResetColor();
    Environment.Exit(1);
}

var sourceCode = await File.ReadAllTextAsync(targetFile);
var fileName = Path.GetFileName(targetFile);

// ── Banner ────────────────────────────────────────────────────────────────────
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   Multi-Agent Analysis System        ║");
Console.WriteLine("║   Powered by GitHub Copilot SDK      ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine($"Target:  {Path.GetFullPath(targetFile)}");
Console.WriteLine("Agents:  Code Review  |  Documentation  |  Testing");
Console.WriteLine();

// ── Run pipeline ──────────────────────────────────────────────────────────────
if (!string.IsNullOrWhiteSpace(config.GithubToken))
    Environment.SetEnvironmentVariable("GITHUB_TOKEN", config.GithubToken);

await using var client = new CopilotClient();
await client.StartAsync();

var pipeline = new AgentPipeline(client, config.Model);
var report = await pipeline.RunAsync(fileName, sourceCode);

// ── Save report ───────────────────────────────────────────────────────────────
var reportPath = Path.ChangeExtension(targetFile, ".analysis.md");
await File.WriteAllTextAsync(reportPath, report);

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Multi-agent analysis complete!");
Console.WriteLine($"Report saved to: {reportPath}");
Console.ResetColor();
