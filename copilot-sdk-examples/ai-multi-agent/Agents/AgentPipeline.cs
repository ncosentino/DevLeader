using System.Text;
using GitHub.Copilot.SDK;

namespace AiMultiAgent.Agents;

/// <summary>
/// Runs the three specialist agents (code review, documentation, testing) sequentially
/// against the same source file and assembles their outputs into a unified report.
/// Each agent creates its own independent CopilotSession so they do not share context.
/// </summary>
public sealed class AgentPipeline
{
    private readonly CopilotClient _client;
    private readonly string _model;

    public AgentPipeline(CopilotClient client, string model)
    {
        _client = client;
        _model = model;
    }

    public async Task<string> RunAsync(
        string fileName,
        string sourceCode,
        CancellationToken ct = default)
    {
        // Each agent runs sequentially and independently with its own session
        var review = await new CodeReviewAgent(_client, _model).ReviewAsync(fileName, sourceCode, ct);
        var docs = await new DocumentationAgent(_client, _model).GenerateAsync(fileName, sourceCode, ct);
        var tests = await new TestingAgent(_client, _model).SuggestAsync(fileName, sourceCode, ct);

        return BuildReport(fileName, review, docs, tests);
    }

    private static string BuildReport(
        string fileName,
        string codeReview,
        string documentation,
        string tests)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"# Multi-Agent Analysis: `{fileName}`");
        sb.AppendLine();
        sb.AppendLine($"_Generated: {DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss} UTC_");
        sb.AppendLine();

        sb.AppendLine("---");
        sb.AppendLine();
        sb.AppendLine("## Code Review");
        sb.AppendLine();
        sb.AppendLine(codeReview);
        sb.AppendLine();

        sb.AppendLine("---");
        sb.AppendLine();
        sb.AppendLine("## Documentation");
        sb.AppendLine();
        sb.AppendLine(documentation);
        sb.AppendLine();

        sb.AppendLine("---");
        sb.AppendLine();
        sb.AppendLine("## Suggested Tests");
        sb.AppendLine();
        sb.AppendLine(tests);

        return sb.ToString();
    }
}
