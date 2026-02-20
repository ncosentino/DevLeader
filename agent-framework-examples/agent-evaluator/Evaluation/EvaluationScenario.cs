namespace AgentEvaluator.Evaluation;

public class EvaluationScenario
{
    public required string Name { get; init; }
    public required string UserMessage { get; init; }
    public required string ExpectedBehavior { get; init; }
    public List<string> EvaluationCriteria { get; init; } = new();
}
