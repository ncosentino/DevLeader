namespace AiCodingAgent.Configuration;

public sealed class AgentConfig
{
    public const string SectionName = "Agent";

    public string GithubToken { get; init; } = string.Empty;

    public string Model { get; init; } = "gpt-4o";

    public string SystemPrompt { get; init; } =
        "You are an expert .NET coding agent. Analyze code, suggest improvements, and implement " +
        "changes when asked. Use read_file and list_files to explore the codebase before making " +
        "changes. Use write_file to create or update files. Use run_dotnet_build to verify changes " +
        "compile successfully. Always explain what you are doing before taking action.";

    public string WorkingDirectory { get; init; } = ".";
}
