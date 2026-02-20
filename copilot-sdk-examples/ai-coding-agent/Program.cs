using AiCodingAgent.Configuration;
using AiCodingAgent.Tools;
using GitHub.Copilot.SDK;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

// ── Configuration ─────────────────────────────────────────────────────────────
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables("CODING_AGENT_")
    .Build();

var agentConfig = configuration.GetSection(AgentConfig.SectionName).Get<AgentConfig>()
    ?? new AgentConfig();

// ── Resolve working directory ──────────────────────────────────────────────────
var workingDir = args.Length > 0 ? args[0] : agentConfig.WorkingDirectory;

// ── Banner ────────────────────────────────────────────────────────────────────
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   Interactive Coding Agent           ║");
Console.WriteLine("║   Powered by GitHub Copilot SDK      ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine($"Working directory: {Path.GetFullPath(workingDir)}");
Console.WriteLine();

// ── Tools ─────────────────────────────────────────────────────────────────────
var codeTools = new CodeTools(workingDir);

// ── Client & Session ──────────────────────────────────────────────────────────
if (!string.IsNullOrWhiteSpace(agentConfig.GithubToken))
    Environment.SetEnvironmentVariable("GITHUB_TOKEN", agentConfig.GithubToken);

await using var client = new CopilotClient();
await client.StartAsync();

await RunAgentLoopAsync(client, agentConfig, codeTools);

// ── Agent REPL ────────────────────────────────────────────────────────────────
static async Task RunAgentLoopAsync(CopilotClient client, AgentConfig config, CodeTools tools)
{
    PrintHelp();

    CopilotSession? session = null;

    async Task StartNewSessionAsync()
    {
        if (session is not null)
            await session.DisposeAsync();

        session = await client.CreateSessionAsync(new SessionConfig
        {
            Model = config.Model,
            Streaming = true,
            SystemMessage = new SystemMessageConfig
            {
                Mode = SystemMessageMode.Append,
                Content = config.SystemPrompt
            },
            Tools = tools.CreateAll()
        });

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("[New agent session started]");
        Console.ResetColor();
    }

    await StartNewSessionAsync();

    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("agent> ");
        Console.ResetColor();

        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
            continue;

        switch (input.ToLowerInvariant())
        {
            case "/exit":
            case "/quit":
                Console.WriteLine("Agent shutting down. Goodbye!");
                if (session is not null) await session.DisposeAsync();
                return;

            case "/new":
                await StartNewSessionAsync();
                continue;

            case "/help":
                PrintHelp();
                continue;
        }

        Console.WriteLine();
        await ExecuteTaskAsync(session!, input);
        Console.WriteLine();
    }
}

static async Task ExecuteTaskAsync(CopilotSession session, string task)
{
    var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

    session.On(evt =>
    {
        switch (evt)
        {
            case AssistantMessageDeltaEvent delta:
                Console.Write(delta.Data.DeltaContent);
                break;

            case AssistantMessageEvent msg:
                Console.Write(msg.Data.Content);
                break;

            case ToolExecutionStartEvent toolStart:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[Agent calling: {toolStart.Data.ToolName}({toolStart.Data.Arguments})]");
                Console.ResetColor();
                break;

            case SessionIdleEvent:
                Console.WriteLine();
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

    try
    {
        await session.SendAsync(new MessageOptions { Prompt = task });
        await tcs.Task;
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Task execution failed] {ex.Message}");
        Console.ResetColor();
    }
}

static void PrintHelp()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Describe a coding task and the agent will explore your codebase and implement changes.");
    Console.WriteLine();
    Console.WriteLine("Example tasks:");
    Console.WriteLine("  Add XML docs to all public methods in ./src/MyService.cs");
    Console.WriteLine("  Refactor the Calculator class to follow the Single Responsibility Principle");
    Console.WriteLine("  Write a unit test file for ./src/OrderProcessor.cs");
    Console.WriteLine("  Find all TODO comments and summarize them");
    Console.WriteLine();
    Console.WriteLine("Commands:  /new (fresh session)  /exit (quit)  /help (this)");
    Console.WriteLine();
    Console.ResetColor();
}
