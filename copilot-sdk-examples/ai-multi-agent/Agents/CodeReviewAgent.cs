using GitHub.Copilot.SDK;

namespace AiMultiAgent.Agents;

/// <summary>
/// Reviews code for correctness, performance, SOLID principles, and best practices.
/// </summary>
public sealed class CodeReviewAgent : AgentBase
{
    public CodeReviewAgent(CopilotClient client, string model) : base(client, model) { }

    public Task<string> ReviewAsync(string fileName, string sourceCode, CancellationToken ct = default) =>
        RunAsync(
            systemPrompt: """
                You are an expert C# code reviewer with deep knowledge of .NET best practices.
                Review code for: correctness, performance, SOLID principles, naming conventions,
                error handling, async patterns, and security concerns.
                Be specific and actionable. Use Markdown with severity labels:
                - **Critical**: bugs or security issues that must be fixed
                - **Major**: significant design or performance concerns
                - **Minor**: style or minor improvements
                """,
            userMessage: $"""
                Review this C# file: `{fileName}`

                ```csharp
                {sourceCode}
                ```

                Provide a structured code review with specific observations.
                Group findings by severity (Critical / Major / Minor).
                """,
            agentLabel: "Code Review Agent",
            ct: ct);
}
