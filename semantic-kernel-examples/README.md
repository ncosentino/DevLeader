# Semantic Kernel in C# - Examples and Sample AI Applications

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

Sample applications demonstrating Semantic Kernel in C#, built to accompany the Dev Leader blog series. Each subfolder is a standalone .NET 9 console app using `appsettings.json` for AI provider configuration (supports OpenAI and Azure OpenAI).

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

Part of the [Semantic Kernel in C#](https://www.devleader.ca/2026/02/25/semantic-kernel-csharp-complete-guide) blog series on Dev Leader:

- [Semantic Kernel in C# - Complete AI Orchestration Guide](https://www.devleader.ca/2026/02/25/semantic-kernel-in-c-complete-ai-orchestration-guide)
- [Semantic Kernel Plugins in C# - The Complete Guide](https://www.devleader.ca/2026/02/27/semantic-kernel-plugins-in-c-the-complete-guide)
- [Building AI Agents with Semantic Kernel in C# - A Practical Step-by-Step Guide](https://www.devleader.ca/2026/03/09/building-ai-agents-with-semantic-kernel-in-c-a-practical-stepbystep-guide)
- [Build an AI Code Review Bot with Semantic Kernel in C#](https://www.devleader.ca/2026/03/12/build-an-ai-code-review-bot-with-semantic-kernel-in-c)
- [Build an AI Task Planner with Semantic Kernel in C#](https://www.devleader.ca/2026/03/13/build-an-ai-task-planner-with-semantic-kernel-in-c)

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
