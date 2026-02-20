# AI Assistant API

An ASP.NET Core Web API that exposes GitHub Copilot conversations as REST endpoints,
demonstrating how to integrate the GitHub Copilot SDK into a hosted web service.

## Features

- **`POST /chat`** -- Full response: sends a message and waits for the complete reply
- **`GET /chat/stream`** -- Streaming: delivers the reply token-by-token via Server-Sent Events
- **`GET /health`** -- Liveness check
- **Calculator tools** -- Demonstrates AI tool calling in an API context
- **DI-managed lifecycle** -- `CopilotService` registered as both singleton and `IHostedService`

## Setup

Create `appsettings.Development.json` with your GitHub PAT:

```json
{
  "GitHub": {
    "Token": "ghp_your_token_here"
  }
}
```

## Running

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
