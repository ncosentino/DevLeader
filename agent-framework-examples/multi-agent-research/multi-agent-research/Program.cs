using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using MultiAgentResearch.Agents;
using MultiAgentResearch.Orchestration;
using OpenAI;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

var topic = args.Length > 0 && args[0] == "--topic" && args.Length > 1
    ? string.Join(" ", args.Skip(1))
    : "Benefits of dependency injection in .NET";

var apiKey = configuration["AIProvider:ApiKey"];
if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.WriteLine("❌ Error: API key not configured.");
    Console.WriteLine("Please set AIProvider:ApiKey in appsettings.Development.json");
    return 1;
}

IChatClient chatClient;
var providerType = configuration["AIProvider:Type"]?.ToLower();
var modelId = configuration["AIProvider:ModelId"] ?? "gpt-4o-mini";

if (providerType == "azureopenai")
{
    var endpoint = configuration["AIProvider:Endpoint"];
    if (string.IsNullOrWhiteSpace(endpoint))
    {
        Console.WriteLine("❌ Error: Azure OpenAI endpoint not configured.");
        return 1;
    }
    
    chatClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureKeyCredential(apiKey))
        .GetChatClient(modelId)
        .AsIChatClient();
}
else
{
    chatClient = new OpenAIClient(apiKey)
        .GetChatClient(modelId)
        .AsIChatClient();
}

var maxRevisions = int.TryParse(
    configuration["Research:MaxRevisionCycles"], 
    out var max) ? max : 2;

var researchAgent = new ResearchAgent(chatClient);
var criticAgent = new CriticAgent(chatClient);
var writerAgent = new WriterAgent(chatClient);

var orchestrator = new ResearchOrchestrator(
    researchAgent,
    criticAgent,
    writerAgent,
    maxRevisions);

try
{
    var result = await orchestrator.RunAsync(topic);
    
    Console.WriteLine();
    
    var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
    var reportPath = Path.Combine("reports", $"report-{timestamp}.md");
    
    Directory.CreateDirectory("reports");
    await File.WriteAllTextAsync(reportPath, result.FinalReport);
    
    Console.WriteLine($"✅ Research complete! Report saved to: {reportPath}");
    Console.WriteLine($"Word count: {result.WordCount} words");
    Console.WriteLine($"Revision cycles: {result.RevisionCycles}");
    
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
    return 1;
}