using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace ContentPipeline.Pipeline;

public sealed class GrammarAgent
{
    private readonly ChatClientAgent _agent;

    public GrammarAgent(IChatClient chatClient)
    {
        _agent = chatClient.AsAIAgent(
            instructions: "You are an expert grammar and style editor. " +
                         "Review content for grammar, punctuation, sentence structure, clarity, and flow. " +
                         "Suggest improvements for readability and coherence. " +
                         "Point out any awkward phrasing or areas that could be clearer.");
    }

    public async Task<string> ReviewAsync(string content, CancellationToken cancellationToken = default)
    {
        var prompt = $"Review this content for grammar, style, clarity, and flow:\n\n{content}";
        var response = await _agent.RunAsync(prompt);
        
        var firstMessage = response.Messages.FirstOrDefault();
        if (firstMessage?.Contents != null)
        {
            var textContent = firstMessage.Contents
                .OfType<TextContent>()
                .FirstOrDefault();
            return textContent?.Text ?? string.Empty;
        }
        
        return string.Empty;
    }
}
