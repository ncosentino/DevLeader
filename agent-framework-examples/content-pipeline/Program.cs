using Azure;
using Azure.AI.OpenAI;
using ContentPipeline.Pipeline;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables();

var config = builder.Configuration;

var apiKey = config["AIProvider:ApiKey"];
if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.WriteLine("❌ Error: AIProvider:ApiKey is not configured.");
    Console.WriteLine("Please set your API key in appsettings.Development.json or environment variables.");
    return 1;
}

IChatClient chatClient;
var providerType = config["AIProvider:Type"]?.ToLower();
var modelId = config["AIProvider:ModelId"] ?? "gpt-4o-mini";

if (providerType == "azureopenai")
{
    var endpoint = config["AIProvider:Endpoint"];
    if (string.IsNullOrWhiteSpace(endpoint))
    {
        Console.WriteLine("❌ Error: AIProvider:Endpoint is required for Azure OpenAI.");
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

builder.Services.AddSingleton(chatClient);

var app = builder.Build();

var topic = GetTopicFromArgs(args);
if (string.IsNullOrWhiteSpace(topic))
{
    Console.WriteLine("❌ Error: No topic provided.");
    Console.WriteLine("\nUsage:");
    Console.WriteLine("  dotnet run -- --topic \"Your topic here\"");
    Console.WriteLine("\nExample:");
    Console.WriteLine("  dotnet run -- --topic \"Benefits of async programming in C#\"");
    return 1;
}

try
{
    var pipeline = new ContentPipeline.Pipeline.ContentPipeline(chatClient);
    var result = await pipeline.RunAsync(topic);

    Console.WriteLine("✅ Pipeline complete!");
    Console.WriteLine($"📄 Final content saved to: {result.OutputPath}");
    Console.WriteLine($"⏱️  Total duration: {result.Duration.TotalSeconds:F1} seconds");

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Pipeline failed: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    return 1;
}

static string? GetTopicFromArgs(string[] args)
{
    for (int i = 0; i < args.Length - 1; i++)
    {
        if (args[i] == "--topic")
        {
            return args[i + 1];
        }
    }
    return null;
}
