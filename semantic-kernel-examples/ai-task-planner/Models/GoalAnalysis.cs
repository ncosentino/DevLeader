namespace AiTaskPlanner.Models;

public sealed class GoalAnalysis
{
    public string Scope { get; set; } = string.Empty;
    public List<string> Constraints { get; set; } = [];
    public List<string> SuccessCriteria { get; set; } = [];
}
