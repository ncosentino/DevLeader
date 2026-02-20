using Microsoft.Extensions.AI;

namespace MultiAgentResearch.Agents;

public class WriterAgent
{
    private readonly IChatClient _chatClient;
    private readonly ChatOptions? _options;

    public WriterAgent(IChatClient chatClient, ChatOptions? options = null)
    {
        _chatClient = chatClient;
        _options = options;
    }

    public async Task<string> WriteReportAsync(
        string topic, 
        string research, 
        CancellationToken cancellationToken = default)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System, 
                "You are a professional technical writer. Transform research notes into " +
                "clear, engaging, well-structured reports for developers. " +
                "Use proper markdown formatting with headings, bullet points, and code examples where appropriate. " +
                "Write in a professional but accessible tone."),
            new(ChatRole.User, 
                $"Write a well-structured, professional research report on '{topic}' " +
                $"based on this research:\n\n{research}")
        };

        var response = await _chatClient.GetResponseAsync(messages, _options, cancellationToken);
        return response.Text ?? string.Empty;
    }
}
