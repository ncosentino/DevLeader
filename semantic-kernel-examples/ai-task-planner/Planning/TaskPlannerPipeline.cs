using System.Text.Json;
using AiTaskPlanner.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AiTaskPlanner.Planning;

public sealed class TaskPlannerPipeline
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly Kernel _kernel;
    private readonly KernelFunction _analyzeGoalFn;
    private readonly KernelFunction _breakdownFn;
    private readonly KernelFunction _prioritizeFn;

    public TaskPlannerPipeline(Kernel kernel)
    {
        _kernel = kernel;

        // Define planning steps as inline prompt functions using KernelFunctionFactory.
        // This approach avoids plugin classes entirely -- the prompt IS the function.
        _analyzeGoalFn = KernelFunctionFactory.CreateFromPrompt(
            """
            Analyze the following project goal and return a JSON object with this exact structure:
            {
              "scope": "one sentence describing what the project covers",
              "constraints": ["constraint1", "constraint2"],
              "successCriteria": ["criteria1", "criteria2"]
            }

            Goal: {{$goal}}

            Return only valid JSON. No markdown fences, no explanation.
            """,
            functionName: "AnalyzeGoal",
            description: "Analyzes a project goal and extracts scope, constraints, and success criteria");

        _breakdownFn = KernelFunctionFactory.CreateFromPrompt(
            """
            Break down the following project goal into concrete, actionable tasks grouped by phase.

            Goal: {{$goal}}
            Scope: {{$scope}}
            Constraints: {{$constraints}}

            Return a JSON object with this exact structure:
            {
              "phases": [
                {
                  "name": "Phase Name",
                  "tasks": ["Task description 1", "Task description 2"]
                }
              ]
            }

            Use 3-5 phases with 3-6 tasks each. Return only valid JSON. No markdown fences, no explanation.
            """,
            functionName: "BreakdownTasks",
            description: "Breaks a goal into phases and concrete tasks");

        _prioritizeFn = KernelFunctionFactory.CreateFromPrompt(
            """
            For each task in the breakdown below, assign a priority (High, Medium, or Low) and an effort
            estimate in whole hours (integer between 1 and 40).

            Goal: {{$goal}}
            Task breakdown:
            {{$breakdown}}

            Return a JSON object with this exact structure:
            {
              "phases": [
                {
                  "name": "Phase Name",
                  "tasks": [
                    { "name": "Task description", "priority": "High", "estimatedHours": 4 }
                  ]
                }
              ]
            }

            Return only valid JSON. No markdown fences, no explanation.
            """,
            functionName: "PrioritizeAndEstimate",
            description: "Assigns priority and effort estimates to each task");
    }

    public async Task<GoalAnalysis> AnalyzeGoalAsync(
        string goal,
        CancellationToken cancellationToken = default)
    {
        // FunctionChoiceBehavior.None() ensures the LLM returns its answer directly
        // without attempting to call any tools -- critical for deterministic pipelines.
        var settings = new OpenAIPromptExecutionSettings
        {
            ResponseFormat = "json_object",
            FunctionChoiceBehavior = FunctionChoiceBehavior.None()
        };

        var args = new KernelArguments(settings) { ["goal"] = goal };

        var result = await _kernel.InvokeAsync(_analyzeGoalFn, args, cancellationToken);
        var json = result.GetValue<string>() ?? "{}";

        return JsonSerializer.Deserialize<GoalAnalysis>(json, JsonOptions)
            ?? new GoalAnalysis { Scope = goal };
    }

    public async Task<TaskBreakdown> GenerateTaskBreakdownAsync(
        string goal,
        GoalAnalysis analysis,
        CancellationToken cancellationToken = default)
    {
        var settings = new OpenAIPromptExecutionSettings
        {
            ResponseFormat = "json_object",
            FunctionChoiceBehavior = FunctionChoiceBehavior.None()
        };

        // KernelArguments carries typed context between pipeline steps.
        // Each step only receives the data it needs -- no bloated shared state.
        var args = new KernelArguments(settings)
        {
            ["goal"] = goal,
            ["scope"] = analysis.Scope,
            ["constraints"] = string.Join("; ", analysis.Constraints)
        };

        var result = await _kernel.InvokeAsync(_breakdownFn, args, cancellationToken);
        var json = result.GetValue<string>() ?? "{}";

        return JsonSerializer.Deserialize<TaskBreakdown>(json, JsonOptions)
            ?? new TaskBreakdown();
    }

    public async Task<TaskPlan> PrioritizeAndEstimateAsync(
        string goal,
        TaskBreakdown breakdown,
        CancellationToken cancellationToken = default)
    {
        var settings = new OpenAIPromptExecutionSettings
        {
            ResponseFormat = "json_object",
            FunctionChoiceBehavior = FunctionChoiceBehavior.None()
        };

        // Serialize the previous step's output and pass it as a KernelArgument.
        // This is explicit data passing -- no hidden state between steps.
        var breakdownJson = JsonSerializer.Serialize(breakdown, JsonOptions);

        var args = new KernelArguments(settings)
        {
            ["goal"] = goal,
            ["breakdown"] = breakdownJson
        };

        var result = await _kernel.InvokeAsync(_prioritizeFn, args, cancellationToken);
        var json = result.GetValue<string>() ?? "{}";

        return JsonSerializer.Deserialize<TaskPlan>(json, JsonOptions)
            ?? new TaskPlan();
    }
}
