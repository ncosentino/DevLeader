namespace AiCliTool.Configuration;

public sealed class CopilotConfig
{
    public const string SectionName = "Copilot";

    public string Model { get; init; } = "gpt-4o";
    public string? GithubToken { get; init; }
    public string SystemPrompt { get; init; } =
        "You are an expert AI coding assistant for .NET developers. " +
        "You help with code reviews, explaining concepts, writing code, " +
        "and debugging. You have tools to read files and list directories " +
        "when the user wants you to analyze their code. " +
        "Be concise but thorough. Use C# code examples where helpful.";
}
