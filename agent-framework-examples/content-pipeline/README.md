# Microsoft Agent Framework Content Pipeline - Multi-Agent Workflow in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

A .NET 10 console application demonstrating **sequential and parallel agent workflow orchestration** using the Microsoft Agent Framework (MAF). Multiple specialized AI agents collaborate to process content through a multi-stage pipeline.

## Overview

This application showcases how to:
- Create multiple specialized AI agents using MAF
- Orchestrate sequential workflow steps
- Execute parallel agent tasks with `Task.WhenAll`
- Consolidate results from multiple agents
- Build production-ready AI pipelines

## Architecture

The content pipeline consists of four specialized agents:

```
┌─────────────┐
│   Writer    │ Generates initial draft
└──────┬──────┘
       │ (sequential)
       v
┌──────────────────────────────┐
│  Parallel Review Stage       │
│  ┌────────────┐ ┌──────────┐│
│  │Fact Checker│ │ Grammar  ││
│  └────────────┘ └──────────┘│
└──────────────┬───────────────┘
               │ (sequential)
               v
        ┌──────────┐
        │  Editor  │ Consolidates & finalizes
        └──────────┘
```

### Agent Roles

1. **Writer Agent**: Expert technical writer that generates comprehensive article drafts
2. **Fact Checker Agent**: Reviews content for technical accuracy and correctness
3. **Grammar Agent**: Reviews for grammar, style, clarity, and flow
4. **Editor Agent**: Consolidates all feedback to produce polished final version

## Prerequisites

- .NET 10 SDK
- OpenAI API key (or Azure OpenAI credentials)
- Internet connection

## Setup

### 1. Clone or navigate to project

```bash
cd C:\dev\DevLeader\agent-framework-examples\content-pipeline
```

### 2. Configure API credentials

Create `appsettings.Development.json`:

```json
{
  "AIProvider": {
    "Type": "openai",
    "ModelId": "gpt-4o-mini",
    "ApiKey": "YOUR_OPENAI_API_KEY_HERE",
    "Endpoint": ""
  }
}
```

For **Azure OpenAI**, set:
```json
{
  "AIProvider": {
    "Type": "azureopenai",
    "ModelId": "gpt-4o-mini",
    "ApiKey": "YOUR_AZURE_KEY",
    "Endpoint": "https://your-resource.openai.azure.com"
  }
}
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Build the project

```bash
dotnet build
```

## Usage

Run the pipeline with a topic:

```bash
dotnet run -- --topic "Benefits of async programming in C#"
```

### Example Output

```
🚀 Starting content pipeline for: "Benefits of async programming in C#"

[Step 1/4] ✍️  Writer Agent generating draft...
✓ Draft complete

[Step 2/4] Running parallel reviews...
  [2a] 📋 Fact Checker Agent reviewing...
  [2b] 📝 Grammar Agent reviewing...
  ✓ Fact check complete
  ✓ Grammar check complete
✓ Reviews complete

[Step 3/4] ✂️  Editor Agent consolidating and finalizing...
✓ Final content ready

[Step 4/4] 💾 Saving output...
✓ Saved to: C:\...\output\content-20241225-143022.md

✅ Pipeline complete!
📄 Final content saved to: C:\...\output\content-20241225-143022.md
⏱️  Total duration: 23.4 seconds
```

## Output

Generated content is saved to `output/content-{timestamp}.md` with:
- Topic as title
- Generation timestamp
- Final polished article content

## Key Technologies

- **Microsoft.Agents.AI** (1.0.0-rc1) - Core agent framework
- **Microsoft.Extensions.AI** (10.3.0) - Unified AI abstractions
- **Microsoft.Extensions.AI.OpenAI** (10.3.0) - OpenAI provider
- **Azure.AI.OpenAI** (2.1.0) - Azure OpenAI client
- **Microsoft.Extensions.Hosting** (10.3.0) - Configuration and DI

## What This Demonstrates

### MAF Capabilities
- Creating specialized agents with distinct roles via `AsAIAgent()`
- Agent orchestration patterns (sequential + parallel)
- Real-world multi-agent workflow

### Workflow Patterns
- **Sequential execution**: Writer → Reviews → Editor
- **Parallel execution**: Fact Checker + Grammar Agent run simultaneously
- **Result consolidation**: Editor combines feedback from multiple sources

### Production Practices
- Configuration management (appsettings.json)
- Provider abstraction (OpenAI or Azure OpenAI)
- Error handling and user feedback
- File I/O and output management
- Cancellation token support

## Project Structure

```
content-pipeline/
├── Program.cs                    # Entry point, DI setup, orchestration
├── appsettings.json              # Configuration template
├── appsettings.Development.json  # Local config (gitignored)
├── Pipeline/
│   ├── ContentPipeline.cs        # Main orchestration logic
│   ├── WriterAgent.cs            # Draft generation
│   ├── FactCheckerAgent.cs       # Accuracy review
│   ├── GrammarAgent.cs           # Style review
│   └── EditorAgent.cs            # Final consolidation
├── Models/
│   ├── PipelineResult.cs         # Pipeline execution result
│   └── AgentOutput.cs            # Individual agent output
└── README.md
```

## Extending the Pipeline

Add more agents or stages:

```csharp
// Add SEO optimization agent
public sealed class SeoAgent
{
    private readonly IAIAgent _agent;
    
    public SeoAgent(IChatClient chatClient)
    {
        _agent = chatClient.AsAIAgent(
            instructions: "You are an SEO expert...");
    }
}

// Run alongside other parallel reviewers
var seoTask = _seoAgent.ReviewAsync(draft, cancellationToken);
await Task.WhenAll(factTask, grammarTask, seoTask);
```

## Troubleshooting

**No API key configured**
- Ensure `appsettings.Development.json` contains your API key
- Or set environment variable: `AIProvider__ApiKey`

**Build errors**
- Ensure .NET 10 SDK is installed: `dotnet --version`
- Run: `dotnet restore` then `dotnet build`

**Timeout errors**
- Some steps may take 10-30 seconds depending on model and content length
- Larger models (GPT-4) will be slower but higher quality

## Blog Series

- [Microsoft Agent Framework in C# - Complete Developer Guide](https://www.devleader.ca/2026/02/21/microsoft-agent-framework-in-c-complete-developer-guide)
- [Getting Started with Microsoft Agent Framework in C#](https://www.devleader.ca/2026/02/21/getting-started-with-microsoft-agent-framework-in-c)

## Learn More

- [Microsoft Agent Framework Documentation](https://learn.microsoft.com/azure/ai-services/agents/)
- [Microsoft.Extensions.AI](https://devblogs.microsoft.com/dotnet/introducing-microsoft-extensions-ai-preview/)

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
