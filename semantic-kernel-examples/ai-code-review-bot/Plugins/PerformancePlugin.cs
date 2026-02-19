using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiCodeReviewBot.Plugins;

public sealed class PerformancePlugin
{
    [KernelFunction]
    [Description("Reviews C# code for performance issues including unnecessary allocations, inefficient LINQ, missing async patterns, and blocking calls")]
    public async Task<string> ReviewForPerformanceAsync(
        Kernel kernel,
        [Description("The C# source code to analyze for performance issues")] string code)
    {
        var chat = kernel.GetRequiredService<IChatCompletionService>();
        var history = new ChatHistory(
            """
            You are an expert C# performance code reviewer.
            Analyze the provided code for: unnecessary memory allocations, string concatenation in loops,
            inefficient LINQ queries, missing ConfigureAwait, blocking async calls (.Result or .Wait()),
            missing cancellation token propagation, N+1 query patterns, excessive object creation,
            and missed opportunities for Span<T> or ArrayPool usage.
            Be specific — reference line numbers or code constructs when possible.
            Format your response as markdown. Use impact labels: 🔴 High impact, 🟡 Medium impact, 🟢 Low impact.
            If no performance issues are found, state that clearly.
            """);
        history.AddUserMessage($"Review this C# code for performance issues:\n\n```csharp\n{code}\n```");

        var result = await chat.GetChatMessageContentAsync(history);
        return result.Content ?? "No response received from performance review.";
    }
}
