namespace AiTaskPlanner.Models;

public sealed class TaskPhase
{
    public string Name { get; set; } = string.Empty;
    public List<string> Tasks { get; set; } = [];
}

public sealed class TaskBreakdown
{
    public List<TaskPhase> Phases { get; set; } = [];
}
