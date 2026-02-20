using AiDocumentQA.Configuration;
using AiDocumentQA.Documents;
using AiDocumentQA.QA;
using AiDocumentQA.Retrieval;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.InMemory;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

var chatConfig = configuration.GetSection("ChatAI").Get<AIProviderConfig>()
    ?? throw new InvalidOperationException("ChatAI configuration is missing.");

var embeddingConfig = configuration.GetSection("EmbeddingAI").Get<AIProviderConfig>()
    ?? throw new InvalidOperationException("EmbeddingAI configuration is missing.");

// Parse CLI args
string? docsPath = null;
string? question = null;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--docs" && i + 1 < args.Length)
        docsPath = args[++i];
    else if (args[i] == "--question" && i + 1 < args.Length)
        question = args[++i];
}

if (string.IsNullOrWhiteSpace(docsPath))
{
    Console.Error.WriteLine("Usage: ai-document-qa --docs <folder> [--question \"<question>\"]");
    return 1;
}

// Build kernel with both chat completion and text embedding services
var builder = Kernel.CreateBuilder();

if (chatConfig.Type.Equals("azureopenai", StringComparison.OrdinalIgnoreCase))
{
    if (string.IsNullOrWhiteSpace(chatConfig.Endpoint))
        throw new InvalidOperationException("ChatAI Azure OpenAI requires Endpoint.");

    builder.AddAzureOpenAIChatCompletion(
        deploymentName: chatConfig.ModelId,
        endpoint: chatConfig.Endpoint,
        apiKey: chatConfig.ApiKey);
}
else
{
    builder.AddOpenAIChatCompletion(
        modelId: chatConfig.ModelId,
        apiKey: chatConfig.ApiKey);
}

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
builder.Services.AddSingleton<Microsoft.Extensions.VectorData.VectorStore, InMemoryVectorStore>();

var kernel = builder.Build();

Console.WriteLine("AI Document Q&A");
Console.WriteLine($"Chat:      {chatConfig.Type} / {chatConfig.ModelId}");
Console.WriteLine($"Embedding: {embeddingConfig.Type} / {embeddingConfig.ModelId}");
Console.WriteLine($"Docs:      {docsPath}");
Console.WriteLine(new string('-', 60));

// Index documents
Console.WriteLine("Loading and indexing documents...");
var chunks = DocumentLoader.LoadFromDirectory(docsPath).ToList();

if (chunks.Count == 0)
{
    Console.Error.WriteLine("No .txt or .md files found in the specified directory.");
    return 1;
}

var embeddingService = kernel.GetRequiredService<Microsoft.SemanticKernel.Embeddings.ITextEmbeddingGenerationService>();
var vectorStore = kernel.Services.GetRequiredService<Microsoft.Extensions.VectorData.VectorStore>();

var indexer = new DocumentIndexer(embeddingService, vectorStore);
await indexer.IndexDocumentsAsync(chunks);

var answerer = new QuestionAnswerer(indexer, kernel);

Console.WriteLine("Ready.\n");

try
{
    if (!string.IsNullOrWhiteSpace(question))
    {
        // Single question mode
        var answer = await answerer.AnswerAsync(question);
        Console.WriteLine($"Q: {question}");
        Console.WriteLine($"\nA: {answer}");
    }
    else
    {
        // Interactive loop
        Console.WriteLine("Interactive mode -- type a question and press Enter. Type 'exit' to quit.\n");
        while (true)
        {
            Console.Write("Q: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            var answer = await answerer.AnswerAsync(input);
            Console.WriteLine($"\nA: {answer}\n");
        }
    }

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nError: {ex.Message}");
    return 1;
}
