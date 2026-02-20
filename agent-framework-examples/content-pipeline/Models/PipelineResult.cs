namespace ContentPipeline.Models;

public sealed class PipelineResult
{
    public required string Topic { get; init; }
    public required string Draft { get; init; }
    public required string FactCheckFeedback { get; init; }
    public required string GrammarFeedback { get; init; }
    public required string FinalContent { get; init; }
    public required string OutputPath { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public TimeSpan Duration => EndTime - StartTime;
}
