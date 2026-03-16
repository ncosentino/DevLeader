# AI Document Q&A - RAG Pattern with Semantic Kernel in C# and .NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

A .NET 9 console application demonstrating the RAG (Retrieval-Augmented Generation) pattern using Semantic Kernel's vector store and text embedding APIs. Load a folder of documents, ask questions, and get AI-generated answers grounded in your own content.

This example walks through the full RAG pipeline in C#: loading and chunking `.txt` and `.md` files, generating embeddings for each chunk, storing them in an in-memory vector store, and then retrieving the most relevant chunks at query time to augment the LLM prompt. It's a practical, runnable reference for building document-grounded AI applications with Semantic Kernel.

## What It Does

1. Loads `.txt` and `.md` files from a directory
2. Chunks documents by paragraph (max ~400 words per chunk)
3. Generates embeddings for each chunk using a text embedding model
4. Stores chunks in an in-memory vector store
5. Answers questions by retrieving the top-3 similar chunks and augmenting the prompt

## Getting Started

### Prerequisites

- .NET 9 SDK
- Azure OpenAI resource with:
  - A **chat completion** deployment (e.g., `gpt-4o`, `gpt-4.1`)
  - A **text embedding** deployment (e.g., `text-embedding-ada-002`, `text-embedding-3-small`)
- OR OpenAI API key (set `Type` to `openai` in config)

### Setup

Copy `appsettings.Development.json.example` to `appsettings.Development.json` and fill in your credentials:

```json
{
  "ChatAI": {
    "Type": "azureopenai",
    "ModelId": "gpt-4o",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-key"
  },
  "EmbeddingAI": {
    "Type": "azureopenai",
    "ModelId": "text-embedding-ada-002",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-key"
  }
}
```

The `appsettings.Development.json` file is gitignored -- never commit credentials.

### Running the Project

```bash
# Single question
dotnet run -- --docs ./sample-docs --question "What is dependency injection?"

# Interactive Q&A loop
dotnet run -- --docs ./sample-docs

# Point to your own documents folder
dotnet run -- --docs /path/to/your/docs --question "Summarize the main topics"
```

## Key Semantic Kernel APIs

| API | Purpose |
|-----|---------|
| `ITextEmbeddingGenerationService` | Generate float vectors from text |
| `InMemoryVectorStore` | In-process vector store (no external DB) |
| `IVectorStoreRecordCollection<K,V>.UpsertAsync()` | Store a chunk with its embedding |
| `VectorizedSearchAsync()` | Top-K similarity search |
| `KernelFunctionFactory.CreateFromPrompt()` | Inline prompt for the QA step |
| `Kernel.InvokeAsync()` | Run the augmented prompt |

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
