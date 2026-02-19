using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiCodeReviewBot.Plugins;

public sealed class BugDetectionPlugin
{
    [KernelFunction]
    [Description("Reviews C# code for potential bugs including null reference exceptions, off-by-one errors, resource leaks, async/await misuse, and logic errors")]
    public async Task<string> ReviewForBugsAsync(
        Kernel kernel,
        [Description("The C# source code to analyze for bugs")] string code)
    {
        var chat = kernel.GetRequiredService<IChatCompletionService>();
        var history = new ChatHistory(
            """
            You are an expert C# code reviewer specializing in bug detection.
            Analyze the provided code for: null reference exceptions, resource leaks, async/await misuse,
            off-by-one errors, incorrect exception handling, race conditions, uninitialized variables, and logic errors.
            Be specific — reference line numbers or code constructs when possible.
            Format your response as markdown. Use severity labels: 🔴 High, 🟡 Medium, 🟢 Low.
            If no bugs are found, state that clearly.
            """);
        history.AddUserMessage($"Review this C# code for bugs:\n\n```csharp\n{code}\n```");

        var result = await chat.GetChatMessageContentAsync(history);
        return result.Content ?? "No response received from bug detection.";
    }
}
