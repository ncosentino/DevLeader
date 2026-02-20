using GitHub.Copilot.SDK;
using Microsoft.Extensions.AI;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using AiAssistantApi.Tools;

namespace AiAssistantApi.Services;

public sealed class CopilotService : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CopilotService> _logger;
    private CopilotClient? _client;

    public CopilotService(IConfiguration configuration, ILogger<CopilotService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var token = _configuration["GitHub:Token"]
            ?? throw new InvalidOperationException(
                "GitHub:Token configuration is required. Set it in appsettings.Development.json.");

        // ghp_ classic PATs work only via env var, not via CopilotClientOptions.GithubToken
        Environment.SetEnvironmentVariable("GITHUB_TOKEN", token);
        _client = new CopilotClient();
        await _client.StartAsync();
        _logger.LogInformation("CopilotClient started successfully");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_client is not null)
        {
            await _client.DisposeAsync();
            _client = null;
            _logger.LogInformation("CopilotClient stopped");
        }
    }

    // Returns the full reply once the session goes idle
    public async Task<string> ChatAsync(
        string prompt,
        string? systemPrompt = null,
        CancellationToken ct = default)
    {
        EnsureStarted();

        var reply = new System.Text.StringBuilder();
        var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        await using var session = await _client!.CreateSessionAsync(BuildSessionConfig(systemPrompt));

        session.On(evt =>
        {
            switch (evt)
            {
                case AssistantMessageDeltaEvent delta:
                    reply.Append(delta.Data.DeltaContent);
                    break;

                case AssistantMessageEvent msg:
                    reply.Append(msg.Data.Content);
                    break;

                case SessionIdleEvent:
                    tcs.TrySetResult();
                    break;

                case SessionErrorEvent err:
                    tcs.TrySetException(new Exception($"{err.Data.ErrorType}: {err.Data.Message}"));
                    break;
            }
        });

        await session.SendAsync(new MessageOptions { Prompt = prompt });
        using var reg = ct.Register(() => tcs.TrySetCanceled(ct));
        await tcs.Task;

        return reply.ToString();
    }

    // Yields reply chunks as they arrive -- bridged from events to IAsyncEnumerable
    public async IAsyncEnumerable<string> StreamAsync(
        string prompt,
        string? systemPrompt = null,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        EnsureStarted();

        var channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions { SingleWriter = true });

        await using var session = await _client!.CreateSessionAsync(BuildSessionConfig(systemPrompt));

        session.On(evt =>
        {
            switch (evt)
            {
                case AssistantMessageDeltaEvent delta:
                    channel.Writer.TryWrite(delta.Data.DeltaContent);
                    break;

                case AssistantMessageEvent msg:
                    channel.Writer.TryWrite(msg.Data.Content);
                    break;

                case SessionIdleEvent:
                    channel.Writer.TryComplete();
                    break;

                case SessionErrorEvent err:
                    channel.Writer.TryComplete(new Exception($"{err.Data.ErrorType}: {err.Data.Message}"));
                    break;
            }
        });

        await session.SendAsync(new MessageOptions { Prompt = prompt });

        await foreach (var chunk in channel.Reader.ReadAllAsync(ct))
        {
            yield return chunk;
        }
    }

    private SessionConfig BuildSessionConfig(string? systemPrompt) => new()
    {
        Model = _configuration["GitHub:Model"] ?? "gpt-4o",
        Streaming = true,
        SystemMessage = new SystemMessageConfig
        {
            Mode = SystemMessageMode.Append,
            Content = systemPrompt ?? "You are a helpful AI assistant. Be concise and accurate."
        },
        Tools = CalculatorTool.CreateAll()
    };

    private void EnsureStarted()
    {
        if (_client is null)
            throw new InvalidOperationException(
                "CopilotService has not been started. Ensure it is registered as IHostedService.");
    }
}
