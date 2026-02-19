using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiCodeReviewBot.Plugins;

public sealed class StylePlugin
{
    [KernelFunction]
    [Description("Reviews C# code for style, naming conventions, code organization, and .NET best practices")]
    public async Task<string> ReviewForStyleAsync(
        Kernel kernel,
        [Description("The C# source code to analyze for style and best practices")] string code)
    {
        var chat = kernel.GetRequiredService<IChatCompletionService>();
        var history = new ChatHistory(
            """
            You are an expert C# code style reviewer following Microsoft's .NET coding guidelines.
            Analyze the provided code for: naming convention violations (PascalCase, camelCase, _privateField),
            missing XML documentation on public members, overly long methods, poor separation of concerns,
            missing use of modern C# features (records, pattern matching, null-coalescing, etc.),
            magic numbers/strings that should be constants, and unclear variable names.
            Be specific — reference line numbers or code constructs when possible.
            Format your response as markdown. Use priority labels: 🟡 Suggested, 🟢 Minor.
            If no style issues are found, state that clearly.
            """);
        history.AddUserMessage($"Review this C# code for style and best practices:\n\n```csharp\n{code}\n```");

        var result = await chat.GetChatMessageContentAsync(history);
        return result.Content ?? "No response received from style review.";
    }
}
