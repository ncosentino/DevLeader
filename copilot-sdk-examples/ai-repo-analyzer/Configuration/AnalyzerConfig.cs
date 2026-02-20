namespace AiRepoAnalyzer.Configuration;

public sealed class AnalyzerConfig
{
    public const string SectionName = "Analyzer";

    public string GithubToken { get; init; } = string.Empty;

    public string Model { get; init; } = "gpt-4o";

    public string DefaultRepositoryPath{ get; init; } = string.Empty;

    /// <summary>Output path for the Markdown report. Defaults to repo-analysis.md in the repo root.</summary>
    public string? OutputPath { get; init; }
}
