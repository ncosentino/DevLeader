namespace AiMultiAgent.Configuration;

public sealed class MultiAgentConfig
{
    public const string SectionName = "MultiAgent";

    public string GithubToken { get; init; } = string.Empty;

    public string Model { get; init; } = "gpt-4o";
}
