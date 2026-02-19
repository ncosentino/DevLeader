using System.Text;
using AiCodeReviewBot.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AiCodeReviewBot.Agents;

public sealed class ReviewOrchestrator
{
    private readonly ChatCompletionAgent _agent;

    public ReviewOrchestrator(Kernel kernel)
    {
        _agent = new ChatCompletionAgent
        {
            Name = "CodeReviewer",
            Instructions =
                """
                You are a senior C# code reviewer. When given code to review, you MUST:

                1. Call ReviewForBugsAsync with the code to check for bugs and defects
                2. Call ReviewForSecurityAsync with the code to check for security vulnerabilities
                3. Call ReviewForPerformanceAsync with the code to check for performance issues
                4. Call ReviewForStyleAsync with the code to check for style and best practice violations

                After collecting all results, synthesize them into a comprehensive markdown review report with:

                ## Executive Summary
                A 2-3 sentence summary of the overall code quality.

                ## Bugs
                Results from the bug review.

                ## Security
                Results from the security review.

                ## Performance
                Results from the performance review.

                ## Style & Best Practices
                Results from the style review.

                ## Overall Recommendation
                One of: ✅ Approved | 🔄 Needs Minor Changes | ❌ Major Revision Required
                Followed by a brief explanation.
                """,
            Kernel = kernel,
            Arguments = new KernelArguments(new OpenAIPromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            })
        };
    }

    public async Task<ReviewResult> ReviewCodeAsync(
        string code,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var thread = new ChatHistoryAgentThread();
        var prompt = $"Please review the following C# code from '{fileName}':\n\n```csharp\n{code}\n```";

        var sb = new StringBuilder();

        try
        {
            await foreach (var response in _agent.InvokeAsync(prompt, thread, cancellationToken: cancellationToken))
            {
                // SK 1.71.0: InvokeAsync returns AgentResponseItem<ChatMessageContent>
                // Access the underlying ChatMessageContent via .Message
                var content = response.Message?.Content;
                if (!string.IsNullOrEmpty(content))
                    sb.Append(content);
            }

            return new ReviewResult
            {
                FileName = fileName,
                Review = sb.ToString(),
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ReviewResult
            {
                FileName = fileName,
                Review = string.Empty,
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
