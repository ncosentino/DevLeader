using AiCliTool.Configuration;
using AiCliTool.Tools;
using GitHub.Copilot.SDK;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

// ── Configuration ────────────────────────────────────────────────────────────
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables("COPILOT_")
    .Build();

var config = configuration.GetSection(CopilotConfig.SectionName).Get<CopilotConfig>()
    ?? new CopilotConfig();

// ── Banner ────────────────────────────────────────────────────────────────────
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   AI CLI Developer Tool              ║");
Console.WriteLine("║   Powered by GitHub Copilot SDK      ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine();
Console.WriteLine("Type your coding question or command. Type /help for options.");
Console.WriteLine();

// ── Tools ─────────────────────────────────────────────────────────────────────
var fsTools = new FileSystemTools();
var tools = new List<AIFunction>
{
    AIFunctionFactory.Create(fsTools.ReadFile, name: "read_file"),
    AIFunctionFactory.Create(fsTools.ListFiles, name: "list_files"),
    AIFunctionFactory.Create(fsTools.GetCurrentDirectory, name: "get_current_directory"),
};

// ── Client & Session ──────────────────────────────────────────────────────────
var clientOptions = new CopilotClientOptions();
if (!string.IsNullOrWhiteSpace(config.GithubToken))
    clientOptions.GithubToken = config.GithubToken;

await using var client = new CopilotClient(clientOptions);
await client.StartAsync();

await RunInteractiveLoopAsync(client, config, tools);

// ── Interactive REPL ──────────────────────────────────────────────────────────
static async Task RunInteractiveLoopAsync(
    CopilotClient client,
    CopilotConfig config,
    List<AIFunction> tools)
{
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
            Tools = tools
        });

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("[New session started]");
        Console.ResetColor();
    }

    await StartNewSessionAsync();

    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("> ");
        Console.ResetColor();

        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
            continue;

        // Handle slash commands
        if (input.StartsWith('/'))
        {
            switch (input.ToLowerInvariant())
            {
                case "/exit":
                case "/quit":
                    Console.WriteLine("Goodbye!");
                    if (session is not null)
                        await session.DisposeAsync();
                    return;

                case "/clear":
                    await StartNewSessionAsync();
                    continue;

                case "/help":
                    PrintHelp();
                    continue;

                default:
                    Console.WriteLine($"Unknown command: {input}. Type /help for options.");
                    continue;
            }
        }

        // Send to AI
        Console.WriteLine();
        await SendMessageAsync(session!, input);
        Console.WriteLine();
    }
}

static async Task SendMessageAsync(CopilotSession session, string prompt)
{
    var tcs = new TaskCompletionSource();

    session.On(evt =>
    {
        switch (evt)
        {
            case AssistantMessageDeltaEvent delta:
                Console.Write(delta.Data.DeltaContent);
                break;

            case AssistantMessageEvent msg:
                // Non-streaming fallback
                Console.Write(msg.Data.Content);
                break;

            case ToolExecutionStartEvent toolStart:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[Tool: {toolStart.Data.ToolName}({toolStart.Data.Arguments})]");
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
        await session.SendAsync(new MessageOptions { Prompt = prompt });
        await tcs.Task;
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Error sending message] {ex.Message}");
        Console.ResetColor();
    }
}

static void PrintHelp()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine();
    Console.WriteLine("Commands:");
    Console.WriteLine("  /clear   -- Start a new conversation session (resets context)");
    Console.WriteLine("  /exit    -- Exit the application");
    Console.WriteLine("  /quit    -- Exit the application");
    Console.WriteLine("  /help    -- Show this help message");
    Console.WriteLine();
    Console.WriteLine("AI Tools available:");
    Console.WriteLine("  read_file <path>           -- Read a source file for AI analysis");
    Console.WriteLine("  list_files <dir> [pattern] -- List files in a directory");
    Console.WriteLine("  get_current_directory      -- Show current working directory");
    Console.WriteLine();
    Console.WriteLine("Example prompts:");
    Console.WriteLine("  Review the code in ./src/MyService.cs");
    Console.WriteLine("  What files are in the current directory?");
    Console.WriteLine("  Explain async/await in C#");
    Console.WriteLine("  Write a unit test for a repository pattern");
    Console.ResetColor();
}
