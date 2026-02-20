using ContentPipeline.Models;
using Microsoft.Extensions.AI;

namespace ContentPipeline.Pipeline;

public sealed class ContentPipeline
{
    private readonly WriterAgent _writerAgent;
    private readonly FactCheckerAgent _factCheckerAgent;
    private readonly GrammarAgent _grammarAgent;
    private readonly EditorAgent _editorAgent;

    public ContentPipeline(IChatClient chatClient)
    {
        _writerAgent = new WriterAgent(chatClient);
        _factCheckerAgent = new FactCheckerAgent(chatClient);
        _grammarAgent = new GrammarAgent(chatClient);
        _editorAgent = new EditorAgent(chatClient);
    }

    public async Task<PipelineResult> RunAsync(string topic, CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;

        Console.WriteLine($"\n🚀 Starting content pipeline for: \"{topic}\"\n");

        Console.WriteLine("[Step 1/4] ✍️  Writer Agent generating draft...");
        var draft = await _writerAgent.GenerateDraftAsync(topic, cancellationToken);
        Console.WriteLine("✓ Draft complete\n");

        Console.WriteLine("[Step 2/4] Running parallel reviews...");
        var (factCheck, grammarCheck) = await RunParallelReviewsAsync(draft, cancellationToken);
        Console.WriteLine("✓ Reviews complete\n");

        Console.WriteLine("[Step 3/4] ✂️  Editor Agent consolidating and finalizing...");
        var finalContent = await _editorAgent.FinalizeAsync(draft, factCheck, grammarCheck, cancellationToken);
        Console.WriteLine("✓ Final content ready\n");

        Console.WriteLine("[Step 4/4] 💾 Saving output...");
        var outputPath = await SaveOutputAsync(topic, finalContent);
        Console.WriteLine($"✓ Saved to: {outputPath}\n");

        var endTime = DateTime.UtcNow;

        return new PipelineResult
        {
            Topic = topic,
            Draft = draft,
            FactCheckFeedback = factCheck,
            GrammarFeedback = grammarCheck,
            FinalContent = finalContent,
            OutputPath = outputPath,
            StartTime = startTime,
            EndTime = endTime
        };
    }

    private async Task<(string factCheck, string grammarCheck)> RunParallelReviewsAsync(
        string draft,
        CancellationToken cancellationToken)
    {
        var factTask = Task.Run(async () =>
        {
            Console.WriteLine("  [2a] 📋 Fact Checker Agent reviewing...");
            var result = await _factCheckerAgent.ReviewAsync(draft, cancellationToken);
            Console.WriteLine("  ✓ Fact check complete");
            return result;
        }, cancellationToken);

        var grammarTask = Task.Run(async () =>
        {
            Console.WriteLine("  [2b] 📝 Grammar Agent reviewing...");
            var result = await _grammarAgent.ReviewAsync(draft, cancellationToken);
            Console.WriteLine("  ✓ Grammar check complete");
            return result;
        }, cancellationToken);

        await Task.WhenAll(factTask, grammarTask);

        return (await factTask, await grammarTask);
    }

    private static async Task<string> SaveOutputAsync(string topic, string content)
    {
        var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
        var fileName = $"content-{timestamp}.md";
        var filePath = Path.Combine(outputDir, fileName);

        var fileContent = $"""
            # {topic}
            
            Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC
            
            ---
            
            {content}
            """;

        await File.WriteAllTextAsync(filePath, fileContent);
        return filePath;
    }
}
