using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace ContentPipeline.Pipeline;

public sealed class WriterAgent
{
    private readonly ChatClientAgent _agent;

    public WriterAgent(IChatClient chatClient)
    {
        _agent = chatClient.AsAIAgent(
            instructions: "You are an expert technical writer specializing in software development topics. " +
                         "Write clear, accurate, and engaging content that is well-structured with an introduction, " +
                         "main points, and conclusion. Use specific examples where relevant.");
    }

    public async Task<string> GenerateDraftAsync(string topic, CancellationToken cancellationToken = default)
    {
        var prompt = $"Write a comprehensive 500-word technical article about: {topic}";
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
