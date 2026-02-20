using GitHub.Copilot.SDK;

namespace AiMultiAgent.Agents;

/// <summary>
/// Generates XML documentation comments and a usage summary for a C# file.
/// </summary>
public sealed class DocumentationAgent : AgentBase
{
    public DocumentationAgent(CopilotClient client, string model) : base(client, model) { }

    public Task<string> GenerateAsync(string fileName, string sourceCode, CancellationToken ct = default) =>
        RunAsync(
            systemPrompt: """
                You are a technical documentation specialist for C# and .NET.
                Generate clear, accurate XML documentation comments for public members.
                Focus on WHAT the code does -- not HOW it does it internally.
                Format output as Markdown containing ready-to-use XML doc comment snippets.
                """,
            userMessage: $"""
                Generate documentation for: `{fileName}`

                ```csharp
                {sourceCode}
                ```

                Provide:
                1. A high-level summary of what this file/class does
                2. XML `<summary>`, `<param>`, and `<returns>` comments for all public members
                3. A usage example showing the typical calling pattern
                """,
            agentLabel: "Documentation Agent",
            ct: ct);
}
