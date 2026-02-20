using System.Text;
using AiTaskPlanner.Configuration;
using AiTaskPlanner.Planning;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var providerConfig = configuration.GetSection("AIProvider").Get<AIProviderConfig>()
    ?? throw new InvalidOperationException("AIProvider configuration is missing.");

string? goal = null;
string? outputPath = null;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--goal" && i + 1 < args.Length)
        goal = args[++i];
    else if (args[i] == "--output" && i + 1 < args.Length)
        outputPath = args[++i];
}

if (string.IsNullOrWhiteSpace(goal))
{
    Console.Error.WriteLine("Usage: ai-task-planner --goal \"<your project goal>\" [--output plan.md]");
    return 1;
}

var builder = Kernel.CreateBuilder();

if (providerConfig.Type.Equals("azureopenai", StringComparison.OrdinalIgnoreCase))
{
    if (string.IsNullOrWhiteSpace(providerConfig.Endpoint))
        throw new InvalidOperationException("Azure OpenAI requires Endpoint in configuration.");

    builder.AddAzureOpenAIChatCompletion(
        deploymentName: providerConfig.ModelId,
        endpoint: providerConfig.Endpoint,
        apiKey: providerConfig.ApiKey);
}
else
{
    builder.AddOpenAIChatCompletion(
        modelId: providerConfig.ModelId,
        apiKey: providerConfig.ApiKey);
}

var kernel = builder.Build();

Console.WriteLine("AI Task Planner");
Console.WriteLine($"Goal: {goal}");
Console.WriteLine($"Provider: {providerConfig.Type} | Model: {providerConfig.ModelId}");
Console.WriteLine(new string('-', 60));

var pipeline = new TaskPlannerPipeline(kernel);

try
{
    Console.Write("Step 1/3: Analyzing goal...");
    var analysis = await pipeline.AnalyzeGoalAsync(goal);
    Console.WriteLine(" ✓");

    Console.Write("Step 2/3: Breaking down into tasks...");
    var breakdown = await pipeline.GenerateTaskBreakdownAsync(goal, analysis);
    Console.WriteLine(" ✓");

    Console.Write("Step 3/3: Prioritizing and estimating effort...");
    var plan = await pipeline.PrioritizeAndEstimateAsync(goal, breakdown);
    Console.WriteLine(" ✓");

    // Build markdown report
    var sb = new StringBuilder();
    sb.AppendLine($"# Task Plan: {goal}");
    sb.AppendLine();
    sb.AppendLine("## Goal Analysis");
    sb.AppendLine($"**Scope:** {analysis.Scope}");
    sb.AppendLine();

    if (analysis.Constraints.Count > 0)
    {
        sb.AppendLine("**Constraints:**");
        foreach (var c in analysis.Constraints)
            sb.AppendLine($"- {c}");
        sb.AppendLine();
    }

    if (analysis.SuccessCriteria.Count > 0)
    {
        sb.AppendLine("**Success Criteria:**");
        foreach (var s in analysis.SuccessCriteria)
            sb.AppendLine($"- {s}");
        sb.AppendLine();
    }

    int totalHours = 0;
    foreach (var phase in plan.Phases)
    {
        sb.AppendLine($"## {phase.Name}");
        foreach (var task in phase.Tasks)
        {
            sb.AppendLine($"- [{task.Priority}] {task.Name} -- {task.EstimatedHours}h");
            totalHours += task.EstimatedHours;
        }
        sb.AppendLine();
    }

    sb.AppendLine("---");
    sb.AppendLine($"**Total estimated effort:** {totalHours}h");

    var report = sb.ToString();

    if (!string.IsNullOrWhiteSpace(outputPath))
    {
        await File.WriteAllTextAsync(outputPath, report);
        Console.WriteLine($"\nPlan saved to: {outputPath}");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine(report);
    }

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nError: {ex.Message}");
    return 1;
}
