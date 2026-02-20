using AgentEvaluator.Agents;
using AgentEvaluator.Evaluation;
using AgentEvaluator.Reports;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

var providerType = configuration["AIProvider:Type"]?.ToLower() ?? "openai";
var modelId = configuration["AIProvider:ModelId"] ?? "gpt-4o-mini";
var apiKey = configuration["AIProvider:ApiKey"];
var endpoint = configuration["AIProvider:Endpoint"];
var evaluatorModelId = configuration["Evaluation:EvaluatorModelId"] ?? modelId;

if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.WriteLine("❌ Error: AI provider API key not configured.");
    Console.WriteLine("Please set the API key in appsettings.Development.json");
    return 1;
}

ChatClient subjectChatClient;
ChatClient judgeChatClient;

if (providerType == "azureopenai")
{
    var azureClient = new AzureOpenAIClient(new Uri(endpoint!), new AzureKeyCredential(apiKey));
    subjectChatClient = azureClient.GetChatClient(modelId);
    judgeChatClient = azureClient.GetChatClient(evaluatorModelId);
}
else
{
    var openAiClient = new OpenAIClient(new ApiKeyCredential(apiKey));
    subjectChatClient = openAiClient.GetChatClient(modelId);
    judgeChatClient = openAiClient.GetChatClient(evaluatorModelId);
}

var subjectAgent = new SubjectAgent(subjectChatClient);
var harness = new EvaluationHarness(subjectAgent, judgeChatClient, configuration);
var scenarios = ScenarioDefinitions.GetDefaultScenarios();

try
{
    var results = await harness.RunAsync(scenarios);

    Console.WriteLine("📊 Evaluation Results Summary:");
    Console.WriteLine($"Overall Quality Score: {results.AverageScore:F1}/10");
    Console.WriteLine($"Passed: {results.PassedScenarios}/{results.TotalScenarios} scenarios");
    Console.WriteLine();

    var outputDirectory = configuration["Evaluation:OutputDirectory"] ?? "reports";
    var reportWriter = new ReportWriter(outputDirectory);
    var (jsonPath, markdownPath) = await reportWriter.WriteReportAsync(results, modelId);

    Console.WriteLine($"Detailed reports saved to:");
    Console.WriteLine($"  - {jsonPath}");
    Console.WriteLine($"  - {markdownPath}");

    return results.PassedScenarios == results.TotalScenarios ? 0 : 1;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error during evaluation: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    return 1;
}
