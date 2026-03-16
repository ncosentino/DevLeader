# AI Semantic Search - Vector Search with Semantic Kernel in C# and .NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

A .NET 9 console application that indexes a corpus of items into an in-memory vector store and provides semantic (meaning-based) search using Semantic Kernel and text embeddings. Search by meaning rather than exact keywords — find relevant results even when the query doesn't match the words in the document.

This example demonstrates how to build a full embedding pipeline in C# using Semantic Kernel's vector store APIs: batch embedding with `ITextEmbeddingGenerationService`, upserting into `VectorStoreCollection<K,V>`, and performing top-k similarity search with optional category filtering via `VectorSearchFilter`. No chat completion model is required — embeddings alone are sufficient for semantic search.

## What It Demonstrates

- **`ITextEmbeddingGenerationService`** -- embed corpus items and queries via Azure OpenAI or OpenAI
- **`CorpusIndexer`** -- batch embed title+body and upsert into `VectorStoreCollection<K,V>`
- **`VectorSearchFilter`** -- filter results by category field before scoring
- **`IVectorSearchable<T>.SearchAsync`** -- top-k similarity search with optional filter
- **Scored results** -- return similarity score alongside each result for ranking transparency
- **Embedding-only app** -- no chat completion model needed

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

## Getting Started

### Prerequisites
- .NET 9 SDK
- Azure OpenAI or OpenAI API access (embedding model only)

### Setup

Copy the example config:

```bash
cp appsettings.Development.json.example appsettings.Development.json
```

Fill in your embedding model credentials:

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

### Running the Project

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

## Newsletter

If you found this useful and you want to learn more about C#, .NET, and software engineering, subscribe to the free Dev Leader Weekly newsletter:

[Subscribe to Dev Leader Weekly](https://weekly.devleader.ca)

## Connect with Dev Leader

- [All Links](https://links.devleader.ca)
- [Website - Dev Leader](https://www.devleader.ca)
- [YouTube - Dev Leader](https://www.youtube.com/@DevLeader)
- [YouTube - Dev Leader Path To Tech](https://www.youtube.com/@DevLeaderPathToTech)
- [YouTube - Dev Leader Podcast](https://www.youtube.com/@DevLeaderPodcast)
- [YouTube - CodeCommute](https://www.youtube.com/@CodeCommute)
- [Newsletter - Dev Leader Weekly](https://weekly.devleader.ca)
- [LinkedIn - Nick Cosentino](https://www.linkedin.com/in/nickcosentino/)
- [GitHub - ncosentino](https://github.com/ncosentino/)
- [Twitter/X - Dev Leader](https://twitter.com/DevLeaderCa)
- [Threads - Dev Leader](https://www.threads.com/@dev.leader)
- [Bluesky - Dev Leader](https://bsky.app/profile/devleader.ca)
- [Mastodon - Dev Leader](https://hachyderm.io/@devleader)
- [Facebook - Dev Leader](https://www.facebook.com/DevLeaderCa)
- [TikTok - Dev Leader](https://www.tiktok.com/@devleader)
- [Twitch - Dev Leader](https://www.twitch.tv/devleaderca)
- [Stack Overflow - Nick Cosentino](https://stackoverflow.com/users/2704424)

---

[![BrandGhost](https://img.shields.io/badge/Powered%20by-BrandGhost-blueviolet?logo=ghost)](https://www.brandghost.ai)

Powered by [BrandGhost](https://www.brandghost.ai) 👻
