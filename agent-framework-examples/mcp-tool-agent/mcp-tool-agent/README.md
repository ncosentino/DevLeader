# MCP Tool Agent

A .NET 10 console application demonstrating the integration of **Microsoft Agent Framework (MAF)** with **Model Context Protocol (MCP)** tools. This application creates an AI agent that can explore and analyze files using MCP filesystem tools.

## What This Demonstrates

This project showcases:
- Creating AI agents using Microsoft Agent Framework
- Integrating MCP (Model Context Protocol) tools with MAF agents
- Supporting multiple AI providers (OpenAI and Azure OpenAI)
- Building interactive and single-shot agent experiences
- Using filesystem tools to analyze directory contents

## Prerequisites

Before running this application, ensure you have:

1. **.NET 10 SDK** installed ([Download](https://dotnet.microsoft.com/download/dotnet/10.0))
2. **Node.js** (for npx command to run MCP filesystem server) ([Download](https://nodejs.org/))
3. **OpenAI API Key** or **Azure OpenAI credentials**
   - Get an OpenAI API key from [platform.openai.com](https://platform.openai.com/)
   - Or configure Azure OpenAI service

## Setup Instructions

### 1. Clone or Download the Project

```bash
cd C:\dev\DevLeader\agent-framework-examples\mcp-tool-agent\mcp-tool-agent
```

### 2. Configure API Keys

Create a file named `appsettings.Development.json` in the project root:

**For OpenAI:**
```json
{
  "AIProvider": {
    "ApiKey": "sk-your-openai-api-key-here"
  }
}
```

**For Azure OpenAI:**
```json
{
  "AIProvider": {
    "Type": "azureopenai",
    "ApiKey": "your-azure-openai-api-key",
    "Endpoint": "https://your-resource.openai.azure.com",
    "ModelId": "your-deployment-name"
  }
}
```

⚠️ **Important:** Never commit `appsettings.Development.json` to source control. It's already included in `.gitignore`.

### 3. Restore Dependencies

```bash
dotnet restore
```

## How to Run

### Interactive Mode (Default)

Start the agent in interactive mode to ask questions about files:

```bash
dotnet run
```

Example session:
```
🤖 MCP Tool Agent - Interactive Mode
Ask questions about files (type 'quit' to exit)

You: What files are in the current directory?
Agent: I found the following files: Program.cs, appsettings.json, mcp-tool-agent.csproj...

You: What does Program.cs do?
Agent: Program.cs is the main entry point that sets up dependency injection...

You: quit
Goodbye!
```

### Single Analysis Mode

Analyze a specific directory with the `--directory` flag:

```bash
dotnet run -- --directory "C:\my-project"
```

Or use the short form:

```bash
dotnet run -- -d "C:\my-project"
```

Example output:
```
🤖 MCP Tool Agent - Analyzing directory: C:\my-project
Using MCP filesystem tools to explore...

📝 Summary:
This project contains 15 C# files organized into 3 directories.
The main components are...
[detailed analysis]
```

## Project Structure

```
mcp-tool-agent/
├── Program.cs                       # Entry point, DI setup, orchestration
├── appsettings.json                 # Configuration (AI provider, MCP server)
├── appsettings.Development.json     # API keys (gitignored)
├── mcp-tool-agent.csproj            # Project file with dependencies
├── Services/
│   ├── McpAgentService.cs          # Agent creation and MCP tool discovery
│   └── AgentRunner.cs              # Agent execution (interactive/single-shot)
└── README.md                        # This file
```

## Configuration Options

### appsettings.json

```json
{
  "AIProvider": {
    "Type": "openai",              // "openai" or "azureopenai"
    "ModelId": "gpt-4o-mini",      // Model to use
    "ApiKey": "",                   // Set in appsettings.Development.json
    "Endpoint": ""                  // Only for Azure OpenAI
  },
  "McpServer": {
    "Type": "stdio",
    "Command": "npx",
    "Args": [
      "-y",
      "@modelcontextprotocol/server-filesystem",
      "{directory}"
    ]
  }
}
```

## Technologies Used

- **Microsoft Agent Framework (MAF) v1.0.0-rc1** - AI agent orchestration
- **Microsoft.Extensions.AI v10.3.0** - AI abstractions and chat client
- **Azure.AI.OpenAI v2.1.0** - OpenAI integration
- **ModelContextProtocol v0.9.0-preview.1** - MCP tool integration
- **.NET 10** - Latest .NET runtime

## How It Works

1. **Configuration Loading**: Reads AI provider settings and MCP server configuration
2. **Chat Client Setup**: Initializes OpenAI or Azure OpenAI chat client
3. **MCP Tool Discovery**: Creates filesystem tools (read_file, list_directory)
4. **Agent Creation**: Uses MAF to create an agent with the chat client and tools
5. **Agent Execution**: Runs in either interactive or single-shot mode
6. **Tool Invocation**: Agent automatically calls MCP tools as needed to answer questions

## Troubleshooting

### "API key not configured"
- Ensure `appsettings.Development.json` exists with your API key
- Check that the API key is valid and not placeholder text

### "Directory not found"
- Verify the directory path exists
- Use absolute paths or ensure relative paths are correct

### "npx command not found"
- Install Node.js from [nodejs.org](https://nodejs.org/)
- Verify npx is available: `npx --version`

### Compilation errors
- Ensure .NET 10 SDK is installed
- Run `dotnet restore` to restore packages
- Check that all package references are restored

## Learn More

### Microsoft Agent Framework
- [Agent Framework Documentation](https://learn.microsoft.com/en-us/dotnet/ai/agents)
- [Microsoft.Extensions.AI Overview](https://learn.microsoft.com/en-us/dotnet/ai/ai-extensions-overview)

### Model Context Protocol
- [MCP Specification](https://modelcontextprotocol.io/)
- [MCP Servers](https://github.com/modelcontextprotocol/servers)
- [Filesystem Server](https://github.com/modelcontextprotocol/servers/tree/main/src/filesystem)

## License

This is a demonstration project for educational purposes.
