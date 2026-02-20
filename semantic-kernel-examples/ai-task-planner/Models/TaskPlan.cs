namespace AiTaskPlanner.Models;

public sealed class PrioritizedTask
{
    public string Name { get; set; } = string.Empty;
    public string Priority { get; set; } = "Medium";
    public int EstimatedHours { get; set; }
}

public sealed class PrioritizedPhase
{
    public string Name { get; set; } = string.Empty;
    public List<PrioritizedTask> Tasks { get; set; } = [];
}

public sealed class TaskPlan
{
    public List<PrioritizedPhase> Phases { get; set; } = [];
}
