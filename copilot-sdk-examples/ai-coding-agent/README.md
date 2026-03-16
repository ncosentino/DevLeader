# Interactive Coding Agent - Autonomous AI Coding Assistant with GitHub Copilot SDK in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

An autonomous AI coding agent built with the GitHub Copilot SDK for .NET. Describe a coding task and the agent will explore your codebase, propose changes, implement them, and verify the build — all with its own set of file system and build tools.

This example shows how to build a fully agentic coding assistant in C# that can take multi-step actions autonomously. It demonstrates tool use patterns with `AIFunctionFactory`, persistent session context across follow-up messages, and how to give an AI agent the ability to read, write, search, and build code within a .NET project.

## Features

- **Explore** -- `read_file`, `list_files` to understand your codebase
- **Implement** -- `write_file` to create or update source files
- **Search** -- `search_in_files` to find patterns across the project
- **Verify** -- `run_dotnet_build` to confirm changes compile
- **Multi-turn** -- persistent session context across follow-up messages

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
  "Agent": {
    "GithubToken": "ghp_your_token_here"
  }
}
```

### Running the Project

```bash
# Use current directory as working directory
dotnet run

# Point at a specific project
dotnet run -- C:\path\to\your\project
```

## Example Tasks

```
agent> Add XML documentation to all public methods in ./MyService.cs
agent> Find all TODO comments and create a task list
agent> Refactor this class to follow the Single Responsibility Principle
agent> Write unit tests for the Calculator class
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
