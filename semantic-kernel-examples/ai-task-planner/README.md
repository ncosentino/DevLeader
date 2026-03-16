# AI Task Planner - Project Planning with Semantic Kernel in C# and .NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

A .NET 9 console application that uses Semantic Kernel to decompose a high-level project goal into a prioritized, estimated task plan using a 3-step sequential pipeline. Given a goal description, the AI analyzes scope, identifies phases, and produces a structured Markdown plan with effort estimates.

This example demonstrates how to build a deterministic AI pipeline using Semantic Kernel without relying on agents or autonomous tool calling. It shows `KernelFunctionFactory.CreateFromPrompt()` for inline prompt definitions, `KernelArguments` for passing context between pipeline steps, and structured JSON output from LLMs — a reliable pattern for multi-step AI workflows in C#.

## Key Concepts Demonstrated

- `KernelFunctionFactory.CreateFromPrompt()` -- define SK functions as inline prompt strings (no plugin class required)
- `Kernel.InvokeAsync()` -- direct function invocation without an agent
- `KernelArguments` -- pass typed context between pipeline steps
- `OpenAIPromptExecutionSettings.ResponseFormat = "json_object"` -- constrain LLM output to structured JSON
- `FunctionChoiceBehavior.None()` -- disable tool calling for deterministic pipelines

## Getting Started

### Prerequisites
- .NET 9 SDK
- Azure OpenAI or OpenAI API access

### Setup

1. Copy `appsettings.Development.json.example` to `appsettings.Development.json`
2. Fill in your Azure OpenAI or OpenAI credentials:

```json
{
  "AIProvider": {
    "Type": "azureopenai",
    "ModelId": "gpt-4.1",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-key"
  }
}
```

### Running the Project

```bash
# Print plan to console
dotnet run -- --goal "Build a REST API for a blog platform"

# Save plan to file
dotnet run -- --goal "Migrate a legacy WinForms app to .NET 9" --output plan.md
```

## Example Output

```
# Task Plan: Build a REST API for a blog platform

## Goal Analysis
**Scope:** Design and implement a RESTful API...

## Phase 1: Foundation
- [High] Define data models and API contracts -- 4h
- [High] Set up .NET 9 Web API project with auth -- 3h

---
**Total estimated effort:** 38h
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
