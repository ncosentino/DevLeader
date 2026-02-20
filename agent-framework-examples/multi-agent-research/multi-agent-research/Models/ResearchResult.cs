namespace MultiAgentResearch.Models;

public class ResearchResult
{
    public required string Topic { get; init; }
    public required string FinalReport { get; init; }
    public int RevisionCycles { get; init; }
    public int WordCount { get; init; }
    public DateTime CompletedAt { get; init; }
}
