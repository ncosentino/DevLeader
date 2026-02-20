# AI Task Planner

A .NET 9 console application that uses Semantic Kernel to decompose a high-level project goal into a prioritized, estimated task plan using a 3-step sequential pipeline.

## Key Concepts Demonstrated

- `KernelFunctionFactory.CreateFromPrompt()` -- define SK functions as inline prompt strings (no plugin class required)
- `Kernel.InvokeAsync()` -- direct function invocation without an agent
- `KernelArguments` -- pass typed context between pipeline steps
- `OpenAIPromptExecutionSettings.ResponseFormat = "json_object"` -- constrain LLM output to structured JSON
- `FunctionChoiceBehavior.None()` -- disable tool calling for deterministic pipelines

## Setup

1. Copy `appsettings.Development.json.example` to `appsettings.Development.json`
2. Fill in your Azure OpenAI or OpenAI credentials

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

## Usage

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
