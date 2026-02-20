using Microsoft.Extensions.AI;

namespace MultiAgentResearch.Agents;

public class ResearchAgent
{
    private readonly IChatClient _chatClient;
    private readonly ChatOptions? _options;

    public ResearchAgent(IChatClient chatClient, ChatOptions? options = null)
    {
        _chatClient = chatClient;
        _options = options;
    }

    public async Task<string> ResearchAsync(string topic, CancellationToken cancellationToken = default)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, 
                "You are an expert researcher. Gather comprehensive, accurate information on topics. " +
                "Include facts, use cases, best practices, and practical examples. " +
                "Provide detailed, well-structured research that covers multiple perspectives."),
            new(ChatRole.User, 
                $"Research the following topic thoroughly: {topic}. " +
                "Cover key concepts, benefits, use cases, and practical examples.")
        };

        var response = await _chatClient.GetResponseAsync(messages, _options, cancellationToken);
        return response.Text ?? string.Empty;
    }

    public async Task<string> ReviseAsync(
        string originalResearch, 
        string critique, 
        CancellationToken cancellationToken = default)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, 
                "You are an expert researcher. Gather comprehensive, accurate information on topics. " +
                "Include facts, use cases, best practices, and practical examples. " +
                "When given critique, address all points raised and improve your research."),
            new(ChatRole.User, 
                $"Based on this critique, improve your research:\n\n" +
                $"Critique:\n{critique}\n\n" +
                $"Your previous research:\n{originalResearch}")
        };

        var response = await _chatClient.GetResponseAsync(messages, _options, cancellationToken);
        return response.Text ?? string.Empty;
    }
}
