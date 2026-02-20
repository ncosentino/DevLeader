using OpenAI.Chat;

namespace AgentEvaluator.Evaluation;

public class LlmJudgeEvaluator
{
    private readonly ChatClient _judgeClient;

    public LlmJudgeEvaluator(ChatClient judgeClient)
    {
        _judgeClient = judgeClient;
    }

    public async Task<EvaluationResult> EvaluateAsync(
        string scenarioName,
        string question,
        string response,
        List<string> criteria)
    {
        var criteriaText = string.Join("\n", criteria.Select((c, i) => $"{i + 1}. {c}"));
        
        var prompt = $"""
            You are an AI response evaluator. Rate the following AI assistant response on a scale of 1-10.
            
            Evaluation Criteria:
            {criteriaText}
            
            Question: {question}
            
            AI Response:
            {response}
            
            Provide your evaluation in the following format:
            SCORE: [number 1-10]
            REASONING: [brief explanation of the score]
            
            Be objective and consider all criteria. A score of 6 or higher indicates acceptable quality.
            """;

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are an objective AI response evaluator."),
            new UserChatMessage(prompt)
        };

        var result = await _judgeClient.CompleteChatAsync(messages);
        var evaluationText = result.Value.Content.FirstOrDefault()?.Text ?? string.Empty;

        var score = ExtractScore(evaluationText);
        var reasoning = ExtractReasoning(evaluationText);

        return new EvaluationResult
        {
            ScenarioName = scenarioName,
            Score = score,
            MaxScore = 10.0,
            Reasoning = reasoning,
            Passed = score >= 6.0,
            Question = question,
            Response = response
        };
    }

    private static double ExtractScore(string evaluationText)
    {
        var lines = evaluationText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            if (line.StartsWith("SCORE:", StringComparison.OrdinalIgnoreCase))
            {
                var scoreText = line.Substring(6).Trim();
                var scorePart = new string(scoreText.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
                
                if (double.TryParse(scorePart, out var score))
                {
                    return Math.Clamp(score, 1.0, 10.0);
                }
            }
        }

        return 5.0;
    }

    private static string ExtractReasoning(string evaluationText)
    {
        var lines = evaluationText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var reasoningStarted = false;
        var reasoningLines = new List<string>();

        foreach (var line in lines)
        {
            if (line.StartsWith("REASONING:", StringComparison.OrdinalIgnoreCase))
            {
                reasoningStarted = true;
                var reasoningText = line.Substring(10).Trim();
                if (!string.IsNullOrWhiteSpace(reasoningText))
                {
                    reasoningLines.Add(reasoningText);
                }
            }
            else if (reasoningStarted && !string.IsNullOrWhiteSpace(line))
            {
                reasoningLines.Add(line.Trim());
            }
        }

        return reasoningLines.Any() 
            ? string.Join(" ", reasoningLines) 
            : "No reasoning provided";
    }
}

public class EvaluationResult
{
    public required string ScenarioName { get; init; }
    public double Score { get; init; }
    public double MaxScore { get; init; }
    public string Reasoning { get; init; } = string.Empty;
    public bool Passed { get; init; }
    public string Question { get; init; } = string.Empty;
    public string Response { get; init; } = string.Empty;
}
