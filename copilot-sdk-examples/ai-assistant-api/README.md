# AI Assistant API - GitHub Copilot SDK REST API Integration in ASP.NET Core C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

An ASP.NET Core Web API that exposes GitHub Copilot conversations as REST endpoints, demonstrating how to integrate the GitHub Copilot SDK into a hosted web service in C# and .NET. Supports both full responses and real-time streaming via Server-Sent Events.

This example shows how to properly manage the `CopilotClient` lifecycle in a DI-managed ASP.NET Core application, implement SSE streaming endpoints, and expose AI tool calling (calculator demo) through a clean REST API. It's a practical starting point for building AI-powered backend services with the Copilot SDK.

## Features

- **`POST /chat`** -- Full response: sends a message and waits for the complete reply
- **`GET /chat/stream`** -- Streaming: delivers the reply token-by-token via Server-Sent Events
- **`GET /health`** -- Liveness check
- **Calculator tools** -- Demonstrates AI tool calling in an API context
- **DI-managed lifecycle** -- `CopilotService` registered as both singleton and `IHostedService`

## Getting Started

### Prerequisites
- .NET 9 SDK
- GitHub CLI (`gh`) installed and in PATH
- Active GitHub Copilot subscription
- Authenticated: `gh auth login`

### Setup

Create `appsettings.Development.json` with your GitHub PAT:

```json
{
  "GitHub": {
    "Token": "ghp_your_token_here"
  }
}
```

### Running the Project

```bash
dotnet run
```

## Example Usage

**Full response:**
```bash
curl -X POST http://localhost:5000/chat \
  -H "Content-Type: application/json" \
  -d '{"prompt": "What is 15% of 200?"}'
```

**Streaming:**
```bash
curl "http://localhost:5000/chat/stream?prompt=Explain+async+await+in+C%23"
```

**Custom system prompt:**
```bash
curl -X POST http://localhost:5000/chat \
  -H "Content-Type: application/json" \
  -d '{"prompt": "Review my code", "systemPrompt": "You are a strict code reviewer."}'
```

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
