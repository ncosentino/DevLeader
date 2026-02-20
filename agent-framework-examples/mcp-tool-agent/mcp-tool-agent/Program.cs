using Azure;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mcp_tool_agent.Services;
using OpenAI;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddSingleton<McpAgentService>();
builder.Services.AddSingleton<AgentRunner>();

var host = builder.Build();

try
{
    var config = host.Services.GetRequiredService<IConfiguration>();
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    var mcpAgentService = host.Services.GetRequiredService<McpAgentService>();
    var agentRunner = host.Services.GetRequiredService<AgentRunner>();

    var apiKey = config["AIProvider:ApiKey"];
    if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "your-api-key-here")
    {
        Console.WriteLine("❌ Error: API key not configured");
        Console.WriteLine("\nPlease configure your API key in appsettings.Development.json:");
        Console.WriteLine("""
        {
          "AIProvider": {
            "ApiKey": "your-actual-api-key"
          }
        }
        """);
        return 1;
    }

    var aiProviderType = config["AIProvider:Type"]?.ToLower() ?? "openai";
    var modelId = config["AIProvider:ModelId"] ?? "gpt-4o-mini";

    logger.LogInformation("Initializing AI provider: {Type} with model {Model}", aiProviderType, modelId);

    IChatClient chatClient;
    
    if (aiProviderType == "azureopenai")
    {
        var endpoint = config["AIProvider:Endpoint"];
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            Console.WriteLine("❌ Error: Azure OpenAI endpoint not configured");
            return 1;
        }

        var azureClient = new AzureOpenAIClient(
            new Uri(endpoint),
            new AzureKeyCredential(apiKey));
        
        chatClient = azureClient.GetChatClient(modelId).AsIChatClient();
    }
    else
    {
        var openAIClient = new OpenAIClient(apiKey);
        chatClient = openAIClient.GetChatClient(modelId).AsIChatClient();
    }

    var directoryArg = Array.FindIndex(args, a => a == "--directory" || a == "-d");
    var directoryPath = directoryArg >= 0 && directoryArg < args.Length - 1
        ? args[directoryArg + 1]
        : Directory.GetCurrentDirectory();

    logger.LogInformation("Target directory: {Directory}", directoryPath);

    if (!Directory.Exists(directoryPath))
    {
        Console.WriteLine($"❌ Error: Directory not found: {directoryPath}");
        return 1;
    }

    var (agent, mcpProcess) = await mcpAgentService.CreateAgentWithMcpToolsAsync(
        chatClient,
        directoryPath);

    try
    {
        if (directoryArg >= 0)
        {
            await agentRunner.RunSingleAnalysisAsync(agent, directoryPath);
        }
        else
        {
            await agentRunner.RunInteractiveModeAsync(agent);
        }
    }
    finally
    {
        mcpAgentService.Dispose();
    }

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Fatal error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    return 1;
}
