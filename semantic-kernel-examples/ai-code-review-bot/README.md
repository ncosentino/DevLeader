# AI Code Review Bot - Automated Code Review with Semantic Kernel Agents in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

A .NET 9 console application that reviews local C# files using Semantic Kernel agents and plugins. Point it at a `.cs` file or folder and it will autonomously invoke specialized review plugins for bugs, security, performance, and style — then synthesize the results into a structured Markdown report.

This example demonstrates how to build a `ChatCompletionAgent` with autonomous tool use in Semantic Kernel. It covers `[KernelFunction]` plugin attributes, `FunctionChoiceBehavior.Auto()` for agent-driven plugin selection, `ChatHistoryAgentThread` for conversation management, and provider-agnostic configuration supporting both OpenAI and Azure OpenAI.

## How It Works

1. You point the bot at a `.cs` file or folder
2. A `ChatCompletionAgent` orchestrates four specialized review plugins:
   - **BugDetectionPlugin** -- null refs, logic errors, resource leaks, async misuse
   - **SecurityPlugin** -- injection attacks, hardcoded secrets, input validation
   - **PerformancePlugin** -- allocations, blocking calls, LINQ efficiency
   - **StylePlugin** -- naming conventions, modern C# patterns, code organization
3. The agent calls each plugin autonomously via `FunctionChoiceBehavior.Auto()`
4. Results are synthesized into a structured markdown report

## Getting Started

### Prerequisites
- .NET 9 SDK
- OpenAI or Azure OpenAI API access

### Setup

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

### Running the Project

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
