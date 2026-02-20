# Multi-Agent Research Team

A .NET 10 console application demonstrating multi-agent collaboration using the Microsoft Agent Framework. This application showcases how specialized AI agents can work together to produce comprehensive research reports through an iterative refinement process.

## Overview

The Multi-Agent Research Team coordinates three specialized agents:

1. **Research Agent** - Gathers and synthesizes information on a given topic
2. **Critic Agent** - Reviews research for gaps, weaknesses, and areas of improvement
3. **Writer Agent** - Produces the final polished, well-structured report

The system uses an orchestrated workflow with revision loops to ensure high-quality output:
```
Research → Critique → (if issues found: Revise) → Write Final Report
```

## Features

- Multi-agent collaboration and coordination
- Iterative research refinement based on critique
- Configurable revision cycles
- Support for both OpenAI and Azure OpenAI
- Markdown report generation with timestamps
- Command-line topic specification

## Prerequisites

- .NET 10 SDK
- OpenAI API key or Azure OpenAI credentials

## Setup

1. **Clone or navigate to the project directory:**
   ```bash
   cd C:\dev\DevLeader\agent-framework-examples\multi-agent-research\multi-agent-research
   ```

2. **Configure API credentials:**
   
   Edit `appsettings.Development.json` (this file is gitignored):
   
   For OpenAI:
   ```json
   {
     "AIProvider": {
       "Type": "openai",
       "ModelId": "gpt-4o-mini",
       "ApiKey": "YOUR_OPENAI_API_KEY"
     }
   }
   ```
   
   For Azure OpenAI:
   ```json
   {
     "AIProvider": {
       "Type": "azureopenai",
       "ModelId": "gpt-4o-mini",
       "ApiKey": "YOUR_AZURE_API_KEY",
       "Endpoint": "https://your-resource.openai.azure.com/"
     }
   }
   ```

3. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

## Usage

### Basic Usage (Default Topic)

```bash
dotnet run
```

This will research the default topic: "Benefits of dependency injection in .NET"

### Custom Topic

```bash
dotnet run -- --topic "Your research topic here"
```

### Examples

```bash
dotnet run -- --topic "Benefits of dependency injection in .NET"
dotnet run -- --topic "Introduction to microservices architecture"
dotnet run -- --topic "Async/await best practices in C#"
```

## Sample Output

```
🔬 Multi-Agent Research Team
Topic: Benefits of dependency injection in .NET

🔍 [Researcher] Gathering information...
🔍 [Critic] Reviewing research quality...
  ⚠️  Gaps found. [Researcher] Revising (cycle 1/2)...
🔍 [Critic] Reviewing research quality...
  ✅ Research quality approved!
✍️  [Writer] Crafting final report...

✅ Research complete! Report saved to: reports/report-20250120-143022.md
Word count: 847 words
Revision cycles: 1
```

## Configuration

The application is configured via `appsettings.json`:

| Setting | Description | Default |
|---------|-------------|---------|
| `AIProvider:Type` | AI provider (`openai` or `azureopenai`) | `openai` |
| `AIProvider:ModelId` | Model to use | `gpt-4o-mini` |
| `AIProvider:ApiKey` | API key | (required) |
| `AIProvider:Endpoint` | Azure OpenAI endpoint (Azure only) | - |
| `Research:MaxRevisionCycles` | Maximum number of research revisions | `2` |
| `Research:MinQualityScore` | Minimum quality score (1-10) | `7` |

## Project Structure

```
multi-agent-research/
├── Agents/
│   ├── ResearchAgent.cs       # Gathers research
│   ├── CriticAgent.cs         # Reviews quality
│   └── WriterAgent.cs         # Writes final report
├── Orchestration/
│   └── ResearchOrchestrator.cs # Coordinates agents
├── Models/
│   └── ResearchResult.cs      # Result model
├── Program.cs                  # Entry point
├── appsettings.json           # Configuration
├── appsettings.Development.json # Local config (gitignored)
└── reports/                   # Generated reports
```

## NuGet Packages

- **Microsoft.Agents.AI** (v1.0.0-rc1) - Agent framework
- **Microsoft.Extensions.AI** (v10.3.0) - AI abstractions
- **Microsoft.Extensions.AI.OpenAI** (v10.3.0) - OpenAI integration
- **Microsoft.Extensions.Hosting** (v10.0.3) - Configuration support
- **Azure.AI.OpenAI** (v2.1.0) - Azure OpenAI client

## What This Demonstrates

### Multi-Agent Coordination
- Orchestrating multiple specialized agents
- Agent-to-agent communication via shared context
- Sequential workflow with feedback loops

### Agent Specialization
- Task-specific agent instructions and personas
- Separation of concerns (research vs. critique vs. writing)
- Each agent focuses on its expertise

### Iterative Refinement
- Critique-based revision loops
- Quality assessment and improvement
- Configurable iteration limits

### Microsoft Agent Framework Features
- IChatClient abstraction for AI interaction
- Chat message construction and context management
- Integration with both OpenAI and Azure OpenAI

## Extending the Application

Ideas for enhancement:
- Add a **Citation Agent** to verify and cite sources
- Implement **parallel research** on subtopics
- Add **quality metrics** beyond keyword detection
- Store research history and enable **comparison reports**
- Integrate with **vector databases** for RAG capabilities
- Add **web search** capabilities to agents

## Troubleshooting

**"API key not configured" error:**
- Ensure `appsettings.Development.json` exists and contains your API key

**Connection errors:**
- Verify your API key is valid
- Check your internet connection
- For Azure OpenAI, verify the endpoint URL is correct

**Low-quality reports:**
- Increase `MaxRevisionCycles` in configuration
- Try a more capable model (e.g., `gpt-4`)
- Provide more specific topics

## License

This is a demonstration project for educational purposes.

## Author

Created as part of the DevLeader blog series on AI agents and .NET.
