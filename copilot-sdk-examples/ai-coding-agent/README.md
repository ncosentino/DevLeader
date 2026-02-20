# Interactive Coding Agent

An autonomous AI coding agent powered by the GitHub Copilot SDK. Describe a coding task
and the agent will explore your codebase, propose changes, implement them, and verify
the build -- all with its own set of file system and build tools.

## Features

- **Explore** -- `read_file`, `list_files` to understand your codebase
- **Implement** -- `write_file` to create or update source files
- **Search** -- `search_in_files` to find patterns across the project
- **Verify** -- `run_dotnet_build` to confirm changes compile
- **Multi-turn** -- persistent session context across follow-up messages

## Setup

Create `appsettings.Development.json`:

```json
{
  "Agent": {
    "GithubToken": "ghp_your_token_here"
  }
}
```

## Running

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
