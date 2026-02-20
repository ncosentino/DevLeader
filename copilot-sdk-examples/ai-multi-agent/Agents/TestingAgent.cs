using GitHub.Copilot.SDK;

namespace AiMultiAgent.Agents;

/// <summary>
/// Suggests xUnit unit tests covering happy paths, edge cases, and error conditions.
/// </summary>
public sealed class TestingAgent : AgentBase
{
    public TestingAgent(CopilotClient client, string model) : base(client, model) { }

    public Task<string> SuggestAsync(string fileName, string sourceCode, CancellationToken ct = default) =>
        RunAsync(
            systemPrompt: """
                You are an expert in .NET testing with xUnit v3 and Moq.
                Write complete, compilable xUnit test methods following the AAA pattern
                (Arrange-Act-Assert). Use the Given_When_Then naming convention.
                Cover: happy paths, boundary values, null inputs, and exception scenarios.
                """,
            userMessage: $"""
                Write unit tests for: `{fileName}`

                ```csharp
                {sourceCode}
                ```

                Produce complete xUnit test class(es) with:
                - All necessary using statements
                - Mock setup where dependencies exist
                - At least one test per public method
                - Edge cases and error condition tests
                """,
            agentLabel: "Testing Agent",
            ct: ct);
}
