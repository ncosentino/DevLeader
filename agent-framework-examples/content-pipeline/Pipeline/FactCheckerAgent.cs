using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace ContentPipeline.Pipeline;

public sealed class FactCheckerAgent
{
    private readonly ChatClientAgent _agent;

    public FactCheckerAgent(IChatClient chatClient)
    {
        _agent = chatClient.AsAIAgent(
            instructions: "You are a meticulous fact checker and technical reviewer. " +
                         "Review content for factual accuracy, technical correctness, and completeness. " +
                         "List any concerns, inaccuracies, or areas that need clarification. " +
                         "If everything is accurate, state that clearly.");
    }

    public async Task<string> ReviewAsync(string content, CancellationToken cancellationToken = default)
    {
        var prompt = $"Review this technical content for factual accuracy and technical correctness:\n\n{content}";
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
