using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiCodeReviewBot.Plugins;

public sealed class SecurityPlugin
{
    [KernelFunction]
    [Description("Reviews C# code for security vulnerabilities including injection attacks, insecure deserialization, hardcoded credentials, and improper input validation")]
    public async Task<string> ReviewForSecurityAsync(
        Kernel kernel,
        [Description("The C# source code to analyze for security vulnerabilities")] string code)
    {
        var chat = kernel.GetRequiredService<IChatCompletionService>();
        var history = new ChatHistory(
            """
            You are an expert C# security code reviewer.
            Analyze the provided code for: SQL injection, XSS, CSRF vulnerabilities, hardcoded secrets or credentials,
            insecure deserialization, path traversal, improper authentication/authorization, insecure random number usage,
            sensitive data exposure, and missing input validation.
            Be specific — reference line numbers or code constructs when possible.
            Format your response as markdown. Use severity labels: 🔴 Critical, 🟠 High, 🟡 Medium, 🟢 Low.
            If no security issues are found, state that clearly.
            """);
        history.AddUserMessage($"Review this C# code for security vulnerabilities:\n\n```csharp\n{code}\n```");

        var result = await chat.GetChatMessageContentAsync(history);
        return result.Content ?? "No response received from security review.";
    }
}
