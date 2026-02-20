# AI Document Q&A with Semantic Kernel

A .NET 9 console application demonstrating the RAG (Retrieval-Augmented Generation) pattern using Semantic Kernel's vector store and text embedding APIs.

## What It Does

1. Loads `.txt` and `.md` files from a directory
2. Chunks documents by paragraph (max ~400 words per chunk)
3. Generates embeddings for each chunk using a text embedding model
4. Stores chunks in an in-memory vector store
5. Answers questions by retrieving the top-3 similar chunks and augmenting the prompt

## Prerequisites

- .NET 9 SDK
- Azure OpenAI resource with:
  - A **chat completion** deployment (e.g., `gpt-4o`, `gpt-4.1`)
  - A **text embedding** deployment (e.g., `text-embedding-ada-002`, `text-embedding-3-small`)
- OR OpenAI API key (set `Type` to `openai` in config)

## Configuration

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

## Usage

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

## Source

Part of the [Dev Leader Semantic Kernel Examples](https://github.com/devleader/semantic-kernel-examples) series.
