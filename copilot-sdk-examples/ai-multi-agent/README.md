# AI Multi-Agent Analysis System - Multi-Agent Workflows with GitHub Copilot SDK in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

A multi-agent system powered by the GitHub Copilot SDK for .NET. Three independent AI agents — each with its own specialized system prompt and `CopilotSession` — analyze a C# source file in sequence and produce a unified Markdown report covering code review, documentation, and test suggestions.

This example demonstrates how to build a multi-agent pipeline in C# where each agent has complete persona isolation using `SystemMessageMode.Replace`. The `AgentPipeline` runs agents sequentially and merges their outputs, showing a practical pattern for composing specialized AI agents in a .NET application.

## Agents

| Agent | Role |
|-------|------|
| **Code Review Agent** | Reviews for correctness, SOLID principles, performance, error handling |
| **Documentation Agent** | Generates XML doc comments and usage examples |
| **Testing Agent** | Suggests xUnit tests covering happy paths and edge cases |

## Getting Started

### Prerequisites
- .NET 9 SDK
- GitHub CLI (`gh`) installed and in PATH
- Active GitHub Copilot subscription
- Authenticated: `gh auth login`

### Setup

Create `appsettings.Development.json`:

```json
{
  "MultiAgent": {
    "GithubToken": "ghp_your_token_here"
  }
}
```

### Running the Project

```bash
# Analyze a specific file
dotnet run -- C:\path\to\MyService.cs

# Interactive (prompts for file path)
dotnet run
```

## Output

Saves `MyService.analysis.md` next to the target file with three sections:
- **Code Review** -- findings grouped by severity (Critical / Major / Minor)
- **Documentation** -- XML doc comments for all public members
- **Suggested Tests** -- compilable xUnit test methods

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
