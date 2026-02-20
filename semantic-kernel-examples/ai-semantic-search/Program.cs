using AiSemanticSearch.Configuration;
using AiSemanticSearch.Indexing;
using AiSemanticSearch.Models;
using AiSemanticSearch.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.InMemory;
using Microsoft.SemanticKernel.Embeddings;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

var embeddingConfig = configuration.GetSection("EmbeddingAI").Get<AIProviderConfig>()
    ?? throw new InvalidOperationException("EmbeddingAI configuration is missing.");

// Parse CLI args
string? corpusPath = null;
string? query = null;
string? categoryFilter = null;
int topK = 5;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--corpus" && i + 1 < args.Length)
        corpusPath = args[++i];
    else if (args[i] == "--query" && i + 1 < args.Length)
        query = args[++i];
    else if (args[i] == "--category" && i + 1 < args.Length)
        categoryFilter = args[++i];
    else if (args[i] == "--top" && i + 1 < args.Length && int.TryParse(args[i + 1], out int k))
    {
        topK = k;
        i++;
    }
}

// Default to sample corpus if not specified
corpusPath ??= Path.Combine(AppContext.BaseDirectory, "Data", "sample-corpus.json");

// Build kernel -- embedding only (no chat completion needed for semantic search)
var builder = Kernel.CreateBuilder();

if (embeddingConfig.Type.Equals("azureopenai", StringComparison.OrdinalIgnoreCase))
{
    if (string.IsNullOrWhiteSpace(embeddingConfig.Endpoint))
        throw new InvalidOperationException("EmbeddingAI Azure OpenAI requires Endpoint.");

    builder.AddAzureOpenAITextEmbeddingGeneration(
        deploymentName: embeddingConfig.ModelId,
        endpoint: embeddingConfig.Endpoint,
        apiKey: embeddingConfig.ApiKey);
}
else
{
    builder.AddOpenAITextEmbeddingGeneration(
        modelId: embeddingConfig.ModelId,
        apiKey: embeddingConfig.ApiKey);
}

// Register in-memory vector store
builder.Services.AddSingleton<VectorStore, InMemoryVectorStore>();

var kernel = builder.Build();

Console.WriteLine("AI Semantic Search");
Console.WriteLine($"Embedding: {embeddingConfig.Type} / {embeddingConfig.ModelId}");
Console.WriteLine($"Corpus:    {corpusPath}");
Console.WriteLine(new string('-', 60));

// Load corpus from JSON
if (!File.Exists(corpusPath))
{
    Console.Error.WriteLine($"Corpus file not found: {corpusPath}");
    return 1;
}

var corpusJson = await File.ReadAllTextAsync(corpusPath);
var items = JsonSerializer.Deserialize<List<SearchItem>>(corpusJson, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
}) ?? throw new InvalidOperationException("Failed to deserialize corpus.");

// Index the corpus
Console.WriteLine("Indexing corpus...");
var embeddingService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();
var vectorStore = kernel.Services.GetRequiredService<VectorStore>();

var indexer = new CorpusIndexer(embeddingService, vectorStore);
await indexer.IndexAsync(items);

var engine = new SemanticSearchEngine(embeddingService, indexer.Collection);

Console.WriteLine("Ready.\n");

try
{
    if (!string.IsNullOrWhiteSpace(query))
    {
        // Single query mode
        await RunQuery(engine, query, topK, categoryFilter);
    }
    else
    {
        // Interactive mode
        Console.WriteLine("Interactive mode -- type a search query and press Enter. Type 'exit' to quit.");
        if (!string.IsNullOrWhiteSpace(categoryFilter))
            Console.WriteLine($"Category filter: {categoryFilter}");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Search: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            await RunQuery(engine, input, topK, categoryFilter);
            Console.WriteLine();
        }
    }

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nError: {ex.Message}");
    return 1;
}

static async Task RunQuery(
    SemanticSearchEngine engine,
    string query,
    int topK,
    string? category)
{
    var results = await engine.SearchAsync(query, topK, category);

    if (results.Count == 0)
    {
        Console.WriteLine("No results found.");
        return;
    }

    Console.WriteLine($"Top {results.Count} results for: \"{query}\"\n");
    for (int i = 0; i < results.Count; i++)
    {
        var r = results[i];
        // Score is cosine similarity: 1.0 = identical, 0.0 = unrelated
        Console.WriteLine($"  [{i + 1}] ({r.Score:F3}) [{r.Item.Category}] {r.Item.Title}");
        Console.WriteLine($"       {r.Item.Body.Substring(0, Math.Min(120, r.Item.Body.Length))}...");
    }
}
