namespace ContentPipeline.Models;

public sealed class AgentOutput
{
    public required string AgentName { get; init; }
    public required string Content { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
