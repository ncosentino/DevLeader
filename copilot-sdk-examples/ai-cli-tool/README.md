# AI CLI Developer Tool - Interactive AI Assistant with GitHub Copilot SDK in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

An interactive AI coding assistant built with the GitHub Copilot SDK for .NET. Ask it coding questions, have it review your files, and get real-time streaming responses — all from the command line. This project demonstrates how to build a practical developer tool that integrates AI into your daily workflow.

This example covers the full lifecycle of `CopilotClient` and `CopilotSession`, streaming responses via `AssistantMessageDeltaEvent`, exposing file system tools using `AIFunctionFactory`, and implementing a clean REPL loop with session management. It's a complete, working application you can extend for your own use cases.

## Getting Started

### Prerequisites

- .NET 9 SDK
- GitHub CLI (`gh`) installed and in PATH
- Active GitHub Copilot subscription
- Authenticated: `gh auth login`

### Setup

1. Copy the example config:
   ```bash
   cp appsettings.Development.json.example appsettings.Development.json
   ```

2. Set your GitHub token in `appsettings.Development.json` (or set `COPILOT_Copilot__GithubToken` env var):
   ```json
   {
     "Copilot": {
       "GithubToken": "ghp_your_token_here"
     }
   }
   ```

3. Run the app:
   ```bash
   dotnet run
   ```

## Usage

```
> Explain the difference between Task and ValueTask in C#

> What files are in the current directory?

> Review the code in ./MyService.cs

> /clear   -- Start new session
> /help    -- Show all commands
> /exit    -- Quit
```

## Architecture

| File | Purpose |
|------|---------|
| `Program.cs` | CLI entry point, interactive REPL loop |
| `Tools/FileSystemTools.cs` | File system tools exposed via AIFunctionFactory |
| `Configuration/CopilotConfig.cs` | Typed configuration for SDK options |

## Key Patterns Demonstrated

- **CopilotClient + CopilotSession lifecycle** -- singleton client, per-conversation sessions
- **Streaming responses** -- `AssistantMessageDeltaEvent` for typewriter output
- **AIFunctionFactory tools** -- `[Description]` attributes, automatic JSON schema generation
- **Event-driven API** -- `session.On(...)` with switch on event type
- **Session reset** -- `/clear` disposes old session and creates a new one

## Related Resources

### Blog Articles
- [GitHub Copilot SDK - AI CLI Tool in C#](https://www.devleader.ca/2026/03/23/github-copilot-sdk-ai-cli-tool-csharp) - Full walkthrough of building this tool

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
