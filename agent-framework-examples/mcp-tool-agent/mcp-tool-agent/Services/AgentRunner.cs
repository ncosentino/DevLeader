using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace mcp_tool_agent.Services;

public class AgentRunner
{
    private readonly ILogger<AgentRunner> _logger;

    public AgentRunner(ILogger<AgentRunner> logger)
    {
        _logger = logger;
    }

    public async Task RunSingleAnalysisAsync(
        AIAgent agent,
        string directoryPath)
    {
        try
        {
            Console.WriteLine($"🤖 MCP Tool Agent - Analyzing directory: {directoryPath}");
            Console.WriteLine("Using MCP filesystem tools to explore...\n");

            var prompt = $"Please analyze the files in {directoryPath} and provide a comprehensive summary. " +
                        $"List the files, describe their types, and give an overview of what this directory contains.";

            var response = await agent.RunAsync(prompt);

            Console.WriteLine("📝 Summary:");
            Console.WriteLine(response.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during single analysis");
            Console.WriteLine($"❌ Error: {ex.Message}");
        }
    }

    public async Task RunInteractiveModeAsync(AIAgent agent)
    {
        try
        {
            Console.WriteLine("🤖 MCP Tool Agent - Interactive Mode");
            Console.WriteLine("Ask questions about files (type 'quit' to exit)\n");

            while (true)
            {
                Console.Write("You: ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                try
                {
                    var response = await agent.RunAsync(input);
                    Console.WriteLine($"\nAgent: {response}\n");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    Console.WriteLine($"❌ Error: {ex.Message}\n");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in interactive mode");
            Console.WriteLine($"❌ Fatal error: {ex.Message}");
        }
    }
}
