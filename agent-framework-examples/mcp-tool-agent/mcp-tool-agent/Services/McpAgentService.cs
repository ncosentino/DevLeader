using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace mcp_tool_agent.Services;

public class McpAgentService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<McpAgentService> _logger;

    public McpAgentService(
        IConfiguration configuration,
        ILogger<McpAgentService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<(AIAgent Agent, Process? McpProcess)> CreateAgentWithMcpToolsAsync(
        IChatClient chatClient,
        string directoryPath)
    {
        try
        {
            var mcpCommand = _configuration["McpServer:Command"] ?? "npx";
            var mcpArgs = _configuration.GetSection("McpServer:Args").Get<string[]>() ?? [];
            
            var processedArgs = mcpArgs.Select(arg => 
                arg.Replace("{directory}", directoryPath)).ToArray();

            _logger.LogInformation(
                "Starting MCP server: {Command} {Args}", 
                mcpCommand, 
                string.Join(" ", processedArgs));

            var tools = await DiscoverMcpToolsAsync(mcpCommand, processedArgs);
            
            var instructions = 
                "You are a file system analyst. Use the available MCP tools to explore and " +
                "summarize files in the directory. When analyzing files, be thorough and provide " +
                "clear summaries of what you find.";

            var agent = new ChatClientAgent(chatClient, instructions);
            
            _logger.LogInformation("Agent created with {ToolCount} MCP tools and instructions: {Instructions}", 
                tools.Count, instructions);
            
            return (agent, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create agent with MCP tools");
            throw;
        }
    }

    private async Task<List<AIFunction>> DiscoverMcpToolsAsync(
        string command, 
        string[] args)
    {
        var tools = new List<AIFunction>();

        try
        {
            _logger.LogInformation("Attempting to discover MCP tools...");
            
            var fileReadFunction = AIFunctionFactory.Create(
                async (string filePath) =>
                {
                    if (!File.Exists(filePath))
                        return $"File not found: {filePath}";
                    
                    try
                    {
                        var content = await File.ReadAllTextAsync(filePath);
                        return content.Length > 1000 
                            ? content[..1000] + $"\n... (truncated, total {content.Length} chars)"
                            : content;
                    }
                    catch (Exception ex)
                    {
                        return $"Error reading file: {ex.Message}";
                    }
                },
                name: "read_file",
                description: "Reads the content of a file from the filesystem");

            var listDirectoryFunction = AIFunctionFactory.Create(
                (string directoryPath) =>
                {
                    if (!Directory.Exists(directoryPath))
                        return $"Directory not found: {directoryPath}";
                    
                    try
                    {
                        var files = Directory.GetFiles(directoryPath);
                        var directories = Directory.GetDirectories(directoryPath);
                        
                        var result = "Files:\n" + 
                            string.Join("\n", files.Select(f => $"  - {Path.GetFileName(f)}")) +
                            "\n\nDirectories:\n" +
                            string.Join("\n", directories.Select(d => $"  - {Path.GetFileName(d)}"));
                        
                        return result;
                    }
                    catch (Exception ex)
                    {
                        return $"Error listing directory: {ex.Message}";
                    }
                },
                name: "list_directory",
                description: "Lists files and directories in the specified path");

            tools.Add(fileReadFunction);
            tools.Add(listDirectoryFunction);

            _logger.LogInformation("Created {ToolCount} filesystem tools", tools.Count);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not connect to MCP server, using fallback tools");
        }

        return tools;
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing MCP agent service");
    }
}
