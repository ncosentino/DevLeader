# Multi-Agent Analysis System

A multi-agent system powered by the GitHub Copilot SDK. Three independent AI agents --
each with its own specialized system prompt and `CopilotSession` -- analyze a C# source
file in sequence and produce a unified Markdown report.

## Agents

| Agent | Role |
|-------|------|
| **Code Review Agent** | Reviews for correctness, SOLID principles, performance, error handling |
| **Documentation Agent** | Generates XML doc comments and usage examples |
| **Testing Agent** | Suggests xUnit tests covering happy paths and edge cases |

## Architecture

Each agent creates its own `CopilotSession` with `SystemMessageMode.Replace`, ensuring
complete persona isolation. The `AgentPipeline` runs them sequentially and merges outputs.

## Setup

Create `appsettings.Development.json`:

```json
{
  "MultiAgent": {
    "GithubToken": "ghp_your_token_here"
  }
}
```

## Running

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
