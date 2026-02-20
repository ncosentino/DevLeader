using AiRepoAnalyzer.Configuration;
using AiRepoAnalyzer.Tools;
using GitHub.Copilot.SDK;
using Microsoft.Extensions.Configuration;

// ── Configuration ─────────────────────────────────────────────────────────────
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables("REPO_ANALYZER_")
    .Build();

var analyzerConfig = configuration.GetSection(AnalyzerConfig.SectionName).Get<AnalyzerConfig>()
    ?? new AnalyzerConfig();

// ── Resolve target repository ─────────────────────────────────────────────────
var repoPath = args.Length > 0 ? args[0] : analyzerConfig.DefaultRepositoryPath;
if (string.IsNullOrWhiteSpace(repoPath))
    repoPath = Directory.GetCurrentDirectory();

repoPath = Path.GetFullPath(repoPath);
if (!Directory.Exists(repoPath))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[Error] Directory not found: {repoPath}");
    Console.ResetColor();
    Environment.Exit(1);
}

// ── Banner ────────────────────────────────────────────────────────────────────
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   Repository Analysis Bot            ║");
Console.WriteLine("║   Powered by GitHub Copilot SDK      ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine($"Analyzing: {repoPath}");
Console.WriteLine();

// ── Client & tools ────────────────────────────────────────────────────────────
var repoTools = new RepositoryTools(repoPath);

if (!string.IsNullOrWhiteSpace(analyzerConfig.GithubToken))
    Environment.SetEnvironmentVariable("GITHUB_TOKEN", analyzerConfig.GithubToken);

await using var client = new CopilotClient();
await client.StartAsync();

// ── Run analysis ──────────────────────────────────────────────────────────────
var report = await RunAnalysisAsync(client, repoTools, analyzerConfig.Model);

// ── Save report ───────────────────────────────────────────────────────────────
var outputPath = analyzerConfig.OutputPath
    ?? Path.Combine(repoPath, "repo-analysis.md");

await File.WriteAllTextAsync(outputPath, report);
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Analysis complete! Report saved to: {outputPath}");
Console.ResetColor();

// ── Analysis session ──────────────────────────────────────────────────────────
static async Task<string> RunAnalysisAsync(CopilotClient client, RepositoryTools tools, string model)
{
    const string SystemPrompt = """
        You are an expert software architect and code reviewer.
        Analyze the repository using your tools and produce a comprehensive Markdown report.
        Be specific and thorough -- read multiple files to form accurate conclusions.
        Format the report with clear ## headings for each section.
        """;

    const string AnalysisPrompt = """
        Please analyze this repository and generate a comprehensive report.

        Follow these steps in order:
        1. Use list_structure to understand the top-level layout
        2. Use find_files to locate README, solution files, and project files
        3. Read the README and key project files
        4. Read the main entry points (Program.cs or equivalent)
        5. Sample a few representative source files to understand patterns
        6. Use count_usage to identify the most-used patterns/frameworks

        Then write a Markdown report with these sections:
        ## Project Overview
        ## Architecture & Structure
        ## Technologies & Dependencies
        ## Code Patterns & Practices
        ## Observations & Recommendations
        """;

    var reply = new System.Text.StringBuilder();
    var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

    await using var session = await client.CreateSessionAsync(new SessionConfig
    {
        Model = model,
        Streaming = true,
        SystemMessage = new SystemMessageConfig
        {
            Mode = SystemMessageMode.Replace,
            Content = SystemPrompt
        },
        Tools = tools.CreateAll()
    });

    session.On(evt =>
    {
        switch (evt)
        {
            case AssistantMessageDeltaEvent delta:
                Console.Write(delta.Data.DeltaContent);
                reply.Append(delta.Data.DeltaContent);
                break;

            case AssistantMessageEvent msg:
                Console.Write(msg.Data.Content);
                reply.Append(msg.Data.Content);
                break;

            case ToolExecutionStartEvent toolStart:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[Exploring: {toolStart.Data.ToolName}({toolStart.Data.Arguments})]");
                Console.ResetColor();
                break;

            case SessionIdleEvent:
                tcs.TrySetResult();
                break;

            case SessionErrorEvent err:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[Error] {err.Data.ErrorType}: {err.Data.Message}");
                Console.ResetColor();
                tcs.TrySetException(new Exception(err.Data.Message));
                break;
        }
    });

    await session.SendAsync(new MessageOptions { Prompt = AnalysisPrompt });
    await tcs.Task;

    return reply.ToString();
}
