# AI Repository Analyzer - Autonomous Codebase Analysis with GitHub Copilot SDK in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

An AI-powered batch tool built with the GitHub Copilot SDK for .NET that analyzes a repository and generates a comprehensive Markdown report. Point it at any .NET repository and it will autonomously explore the codebase, then produce an architecture overview, pattern analysis, and improvement recommendations.

This example demonstrates how to build an autonomous AI agent using the GitHub Copilot SDK — giving it tools to explore file structures, read source files, and find patterns across a project, then synthesizing all findings into a structured output document. It's a practical demonstration of agentic AI workflows in C#.

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
  "Analyzer": {
    "GithubToken": "ghp_your_token_here"
  }
}
```

### Running the Project

```bash
# Analyze current directory
dotnet run

# Analyze a specific repository
dotnet run -- C:\path\to\your\repo
```

## Output

The bot generates `repo-analysis.md` with these sections:

- **Project Overview** -- What the repository does and its purpose
- **Architecture & Structure** -- How the codebase is organized
- **Technologies & Dependencies** -- Frameworks and packages in use
- **Code Patterns & Practices** -- Design patterns and conventions observed
- **Observations & Recommendations** -- Actionable improvement suggestions

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
