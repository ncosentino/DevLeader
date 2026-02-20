using MultiAgentResearch.Agents;
using MultiAgentResearch.Models;

namespace MultiAgentResearch.Orchestration;

public class ResearchOrchestrator
{
    private readonly ResearchAgent _researcher;
    private readonly CriticAgent _critic;
    private readonly WriterAgent _writer;
    private readonly int _maxRevisions;

    public ResearchOrchestrator(
        ResearchAgent researcher,
        CriticAgent critic,
        WriterAgent writer,
        int maxRevisions = 2)
    {
        _researcher = researcher;
        _critic = critic;
        _writer = writer;
        _maxRevisions = maxRevisions;
    }

    public async Task<ResearchResult> RunAsync(string topic, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"🔬 Multi-Agent Research Team");
        Console.WriteLine($"Topic: {topic}");
        Console.WriteLine();

        Console.WriteLine("🔍 [Researcher] Gathering information...");
        var currentResearch = await _researcher.ResearchAsync(topic, cancellationToken);
        
        int revisionCycles = 0;
        
        for (int cycle = 0; cycle < _maxRevisions; cycle++)
        {
            Console.WriteLine("🔍 [Critic] Reviewing research quality...");
            var critique = await _critic.CritiqueAsync(currentResearch, cancellationToken);
            
            bool needsRevision = 
                critique.Contains("gap", StringComparison.OrdinalIgnoreCase) ||
                critique.Contains("gaps", StringComparison.OrdinalIgnoreCase) ||
                critique.Contains("missing", StringComparison.OrdinalIgnoreCase) ||
                critique.Contains("weak", StringComparison.OrdinalIgnoreCase) ||
                critique.Contains("incomplete", StringComparison.OrdinalIgnoreCase) ||
                critique.Contains("lacking", StringComparison.OrdinalIgnoreCase);
            
            if (!needsRevision)
            {
                Console.WriteLine("  ✅ Research quality approved!");
                break;
            }
            
            revisionCycles++;
            Console.WriteLine($"  ⚠️  Gaps found. [Researcher] Revising (cycle {cycle + 1}/{_maxRevisions})...");
            
            currentResearch = await _researcher.ReviseAsync(
                currentResearch, 
                critique, 
                cancellationToken);
        }
        
        Console.WriteLine("✍️  [Writer] Crafting final report...");
        var finalReport = await _writer.WriteReportAsync(topic, currentResearch, cancellationToken);
        
        var wordCount = finalReport.Split(
            new[] { ' ', '\n', '\r', '\t' }, 
            StringSplitOptions.RemoveEmptyEntries).Length;

        return new ResearchResult
        {
            Topic = topic,
            FinalReport = finalReport,
            RevisionCycles = revisionCycles,
            WordCount = wordCount,
            CompletedAt = DateTime.UtcNow
        };
    }
}
