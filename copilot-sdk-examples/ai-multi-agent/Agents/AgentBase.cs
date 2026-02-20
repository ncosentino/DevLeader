using GitHub.Copilot.SDK;

namespace AiMultiAgent.Agents;

/// <summary>
/// Base class for all agents. Each agent creates its own CopilotSession with a
/// specialized system prompt and runs it to completion before returning the result.
/// </summary>
public abstract class AgentBase
{
    protected readonly CopilotClient Client;
    protected readonly string Model;

    protected AgentBase(CopilotClient client, string model)
    {
        Client = client;
        Model = model;
    }

    /// <summary>
    /// Creates a session with the given system prompt, sends the user message,
    /// waits for the session to go idle, and returns the accumulated reply.
    /// </summary>
    protected async Task<string> RunAsync(
        string systemPrompt,
        string userMessage,
        string agentLabel,
        CancellationToken ct = default)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n[{agentLabel}] Starting...");
        Console.ResetColor();

        var reply = new System.Text.StringBuilder();
        var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        await using var session = await Client.CreateSessionAsync(new SessionConfig
        {
            Model = Model,
            Streaming = true,
            SystemMessage = new SystemMessageConfig
            {
                // Replace ensures each agent has only its own persona -- no inherited context
                Mode = SystemMessageMode.Replace,
                Content = systemPrompt
            }
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

                case SessionIdleEvent:
                    Console.WriteLine();
                    tcs.TrySetResult();
                    break;

                case SessionErrorEvent err:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[{agentLabel} Error] {err.Data.ErrorType}: {err.Data.Message}");
                    Console.ResetColor();
                    tcs.TrySetException(new Exception(err.Data.Message));
                    break;
            }
        });

        await session.SendAsync(new MessageOptions { Prompt = userMessage });
        using var reg = ct.Register(() => tcs.TrySetCanceled(ct));
        await tcs.Task;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[{agentLabel}] Complete.");
        Console.ResetColor();

        return reply.ToString();
    }
}
