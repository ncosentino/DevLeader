# AI CLI Developer Tool

An interactive AI coding assistant built with the GitHub Copilot SDK for .NET.
Ask it coding questions, have it review your files, and get real-time streaming responses.

Part of the [Dev Leader blog series](https://www.devleader.ca/2026/03/23/github-copilot-sdk-ai-cli-tool-csharp) on building real apps with the GitHub Copilot SDK.

## Prerequisites

- .NET 9 SDK
- GitHub CLI (`gh`) installed and in PATH
- Active GitHub Copilot subscription
- Authenticated: `gh auth login`

## Setup

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
