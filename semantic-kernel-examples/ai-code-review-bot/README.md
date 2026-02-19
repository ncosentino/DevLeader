# AI Code Review Bot

A .NET 9 console application that reviews local C# files using Semantic Kernel agents and plugins.

Built to accompany the Dev Leader blog series: [Build an AI Code Review Bot with Semantic Kernel in C#](https://www.devleader.ca)

## How It Works

1. You point the bot at a `.cs` file or folder
2. A `ChatCompletionAgent` orchestrates four specialized review plugins:
   - **BugDetectionPlugin** -- null refs, logic errors, resource leaks, async misuse
   - **SecurityPlugin** -- injection attacks, hardcoded secrets, input validation
   - **PerformancePlugin** -- allocations, blocking calls, LINQ efficiency
   - **StylePlugin** -- naming conventions, modern C# patterns, code organization
3. The agent calls each plugin autonomously via `FunctionChoiceBehavior.Auto()`
4. Results are synthesized into a structured markdown report

## Setup

### 1. Configure your AI provider

Copy the example config and fill in your credentials:

```bash
cp appsettings.Development.json.example appsettings.Development.json
```

**For OpenAI:**
```json
{
  "AIProvider": {
    "Type": "openai",
    "ModelId": "gpt-4o",
    "ApiKey": "sk-..."
  }
}
```

**For Azure OpenAI:**
```json
{
  "AIProvider": {
    "Type": "azureopenai",
    "ModelId": "gpt-4o",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-azure-key"
  }
}
```

Alternatively, set environment variables:
```bash
AICODEBOT_AIProvider__ApiKey=sk-...
AICODEBOT_AIProvider__Type=openai
```

### 2. Build

```bash
dotnet build
```

## Usage

```bash
# Review a single file
dotnet run -- --path src/MyService.cs

# Review all .cs files in a folder
dotnet run -- --path src/

# Save report to a file
dotnet run -- --path src/ --output review-report.md
```

## SK Features Demonstrated

- `[KernelFunction]` and `[Description]` plugin attributes
- `Kernel` parameter injection in plugin functions
- `ChatCompletionAgent` with `Instructions`
- `FunctionChoiceBehavior.Auto()` for autonomous tool use
- `ChatHistoryAgentThread` for conversation management
- `IConfiguration` with `appsettings.json` for provider selection
