# Semantic Kernel Examples

Sample applications demonstrating Semantic Kernel in C#, built to accompany the Dev Leader blog series.

Each subfolder is a standalone .NET 9 console app. All apps use `appsettings.json` for AI provider configuration (supports OpenAI and Azure OpenAI).

## Apps

| App | Description | Blog Article |
|-----|-------------|--------------|
| [ai-code-review-bot](./ai-code-review-bot/) | Reviews local C# files using SK agents + plugins | [Build an AI Code Review Bot with Semantic Kernel](https://www.devleader.ca/2026/03/12/semantic-kernel-ai-code-review-bot-csharp) |
| [ai-task-planner](./ai-task-planner/) | Multi-agent task decomposition and execution | [Build an AI Task Planner with Semantic Kernel](https://www.devleader.ca/2026/03/13/semantic-kernel-ai-task-planner-csharp) |
| [document-qa-app](./document-qa-app/) | Document Q&A using RAG + vector store | [Build a Document Q&A App with RAG and Semantic Kernel](https://www.devleader.ca/2026/03/17/semantic-kernel-document-qa-app-csharp) |
| [semantic-search-engine](./semantic-search-engine/) | Semantic search over a text corpus | [Build a Semantic Search Engine with Semantic Kernel](https://www.devleader.ca/2026/03/18/semantic-kernel-semantic-search-engine-csharp) |

## Configuration

Each app has an `appsettings.json` with this shape:

```json
{
  "AIProvider": {
    "Type": "openai",
    "ModelId": "gpt-4o",
    "ApiKey": "",
    "Endpoint": ""
  }
}
```

Copy `appsettings.Development.json.example` to `appsettings.Development.json` and fill in your credentials. This file is gitignored.

## Blog Series

Part of the [Semantic Kernel in C#](https://www.devleader.ca/2026/02/25/semantic-kernel-csharp-complete-guide) blog series on Dev Leader.
