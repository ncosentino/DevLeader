using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace ContentPipeline.Pipeline;

public sealed class EditorAgent
{
    private readonly ChatClientAgent _agent;

    public EditorAgent(IChatClient chatClient)
    {
        _agent = chatClient.AsAIAgent(
            instructions: "You are a senior editor responsible for producing final polished content. " +
                         "Given a draft and feedback from fact checkers and grammar reviewers, " +
                         "incorporate all valid feedback to produce a refined, accurate, and well-written final version. " +
                         "Maintain the original intent while improving quality.");
    }

    public async Task<string> FinalizeAsync(
        string draft,
        string factCheckFeedback,
        string grammarFeedback,
        CancellationToken cancellationToken = default)
    {
        var prompt = $"""
            Original draft:
            {draft}
            
            Fact checker feedback:
            {factCheckFeedback}
            
            Grammar and style feedback:
            {grammarFeedback}
            
            Please produce a final polished version incorporating all feedback while maintaining technical accuracy and readability.
            """;

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
