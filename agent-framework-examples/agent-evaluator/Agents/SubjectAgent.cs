using OpenAI.Chat;

namespace AgentEvaluator.Agents;

public class SubjectAgent
{
    private readonly ChatClient _chatClient;

    private const string AgentSystemMessage = """
        You are a helpful C# and .NET development assistant.
        Provide clear, accurate, and practical answers to programming questions.
        When providing code examples, ensure they are compilable and follow best practices.
        When explaining concepts, be thorough but concise.
        Always prioritize correctness and clarity in your responses.
        """;

    public SubjectAgent(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<string> GetResponseAsync(string userMessage, CancellationToken cancellationToken = default)
    {
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(AgentSystemMessage),
            new UserChatMessage(userMessage)
        };

        var result = await _chatClient.CompleteChatAsync(messages, cancellationToken: cancellationToken);
        return result.Value.Content.FirstOrDefault()?.Text ?? string.Empty;
    }
}
