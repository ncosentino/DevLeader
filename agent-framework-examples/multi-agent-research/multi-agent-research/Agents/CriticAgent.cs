using Microsoft.Extensions.AI;

namespace MultiAgentResearch.Agents;

public class CriticAgent
{
    private readonly IChatClient _chatClient;
    private readonly ChatOptions? _options;

    public CriticAgent(IChatClient chatClient, ChatOptions? options = null)
    {
        _chatClient = chatClient;
        _options = options;
    }

    public async Task<string> CritiqueAsync(string research, CancellationToken cancellationToken = default)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, 
                "You are a critical reviewer. Identify gaps, inaccuracies, and weaknesses in research. " +
                "Be specific and actionable. Rate research quality on a 1-10 scale. " +
                "Focus on: completeness, accuracy, practical examples, clarity, and depth."),
            new(ChatRole.User, 
                $"Review this research and identify gaps or weaknesses. " +
                $"Rate quality 1-10 and list specific improvements needed:\n\n{research}")
        };

        var response = await _chatClient.GetResponseAsync(messages, _options, cancellationToken);
        return response.Text ?? string.Empty;
    }
}
