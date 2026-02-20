using AgentEvaluator.Agents;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace AgentEvaluator.Evaluation;

public class EvaluationHarness
{
    private readonly SubjectAgent _subjectAgent;
    private readonly LlmJudgeEvaluator _evaluator;
    private readonly double _passThreshold;

    public EvaluationHarness(
        SubjectAgent subjectAgent,
        ChatClient judgeClient,
        IConfiguration configuration)
    {
        _subjectAgent = subjectAgent;
        _evaluator = new LlmJudgeEvaluator(judgeClient);
        _passThreshold = double.TryParse(configuration["Evaluation:PassThreshold"], out var threshold) ? threshold : 6.0;
    }

    public async Task<HarnessResults> RunAsync(List<EvaluationScenario> scenarios)
    {
        Console.WriteLine("🧪 AI Agent Evaluation Harness");
        Console.WriteLine($"Running {scenarios.Count} evaluation scenarios...");
        Console.WriteLine();

        var results = new List<EvaluationResult>();

        for (int i = 0; i < scenarios.Count; i++)
        {
            var scenario = scenarios[i];
            Console.WriteLine($"[{i + 1}/{scenarios.Count}] {scenario.Name}: \"{scenario.UserMessage}\"");

            var agentResponse = await _subjectAgent.GetResponseAsync(scenario.UserMessage);

            var evaluationResult = await _evaluator.EvaluateAsync(
                scenario.Name,
                scenario.UserMessage,
                agentResponse,
                scenario.EvaluationCriteria);

            var statusIcon = evaluationResult.Passed ? "✅" : "❌";
            var status = evaluationResult.Passed ? "Passed" : "Failed";

            Console.WriteLine($"  {statusIcon} Score: {evaluationResult.Score:F1}/{evaluationResult.MaxScore} ({status})");
            Console.WriteLine($"     {evaluationResult.Reasoning}");
            Console.WriteLine();

            results.Add(evaluationResult);
        }

        return new HarnessResults
        {
            Results = results,
            PassThreshold = _passThreshold,
            TotalScenarios = scenarios.Count,
            PassedScenarios = results.Count(r => r.Passed),
            AverageScore = results.Average(r => r.Score)
        };
    }
}

public class HarnessResults
{
    public required List<EvaluationResult> Results { get; init; }
    public required double PassThreshold { get; init; }
    public required int TotalScenarios { get; init; }
    public required int PassedScenarios { get; init; }
    public required double AverageScore { get; init; }
}
