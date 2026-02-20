# AI Semantic Search

A .NET 9 console app that indexes a corpus of items into an in-memory vector store and provides semantic (meaning-based) search using Semantic Kernel and text embeddings.

## What It Demonstrates

- **`ITextEmbeddingGenerationService`** -- embed corpus items and queries via Azure OpenAI or OpenAI
- **`CorpusIndexer`** -- batch embed title+body and upsert into `VectorStoreCollection<K,V>`
- **`VectorSearchFilter`** -- filter results by category field before scoring
- **`IVectorSearchable<T>.SearchAsync`** -- top-k similarity search with optional filter
- **Scored results** -- return similarity score alongside each result for ranking transparency
- **Embedding-only app** -- no chat completion model needed (embedding + search is sufficient)

## Project Structure

```
ai-semantic-search/
├── Configuration/
│   └── AIProviderConfig.cs          # OpenAI or Azure OpenAI config
├── Data/
│   └── sample-corpus.json           # 20 sample FAQ items (SK + DI topics)
├── Indexing/
│   └── CorpusIndexer.cs             # Batch embed + upsert pipeline
├── Models/
│   └── SearchItem.cs                # VectorStoreKey/Data/Vector model
├── Search/
│   └── SemanticSearchEngine.cs      # SearchAsync with filter support
├── Program.cs                       # CLI entry point
├── appsettings.json
└── appsettings.Development.json     # Gitignored; holds your API key
```

## Setup

Copy the example config:

```bash
cp appsettings.Development.json.example appsettings.Development.json
```

Fill in your embedding model credentials in `appsettings.Development.json`:

```json
{
  "EmbeddingAI": {
    "Type": "azureopenai",
    "ModelId": "text-embedding-ada-002",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-api-key"
  }
}
```

For standard OpenAI, set `"Type": "openai"` and omit `Endpoint`.

## Running

```bash
# Interactive search mode (default corpus)
dotnet run

# Single query
dotnet run -- --query "how do I add a plugin to SK"

# Filter by category (concept | howto | troubleshooting)
dotnet run -- --query "agent types" --category concept

# Custom corpus file
dotnet run -- --corpus /path/to/my-corpus.json --query "connection pooling"

# Control result count
dotnet run -- --query "embeddings" --top 3
```

## Custom Corpus

The corpus is a JSON array of objects with these fields:

```json
[
  {
    "Id": "unique-id",
    "Title": "Item title",
    "Body": "Full text content",
    "Category": "optional-category"
  }
]
```

Only `Id`, `Title`, and `Body` are required. `Category` enables filtered search with `--category`.
