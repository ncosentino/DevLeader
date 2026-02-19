# Structured Logging Examples — Serilog and Logging Scopes in ASP.NET Core

[![Dev Leader YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Dev Leader Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Linktree](https://img.shields.io/badge/Linktree-devleader-green?logo=linktree)](https://www.linktr.ee/devleader)

Practical **structured logging** examples for .NET and ASP.NET Core using **Serilog**. Learn how to move beyond plain-text logs with queryable key-value properties, how to use **logging scopes** to add contextual information across multiple log entries, and how to configure Serilog entirely from `appsettings.json` without touching code.

## 📺 Watch the Videos

### STOP Passing State For Logging: A Practical Example of Logging Scopes
[![STOP Passing State For Logging: A Practical Example of Logging Scopes](https://img.youtube.com/vi/tJ8mouFN0Lk/hqdefault.jpg)](https://youtu.be/tJ8mouFN0Lk)

### Beginner's Guide To Structured Logging in C#: Logging Scopes
[![Beginner's Guide To Structured Logging in C#: Logging Scopes](https://img.youtube.com/vi/RZyO54mfQKQ/hqdefault.jpg)](https://youtu.be/RZyO54mfQKQ)

### How To Configure Serilog From AppSettings.json
[![How To Configure Serilog From AppSettings.json](https://img.youtube.com/vi/zMPqMvo7F98/hqdefault.jpg)](https://youtu.be/zMPqMvo7F98)

### Let's VIBE! ChatGPT vs Copilot — Which Integrates Serilog Better?
[![Let's VIBE! ChatGPT vs Copilot — Which Integrates Serilog Better?](https://img.youtube.com/vi/vKepCbmuBfo/hqdefault.jpg)](https://youtu.be/vKepCbmuBfo)

## 🗂️ Projects

| Project | Description |
|---------|-------------|
| `AspNetCoreSerilogFromAppSettingsExample` | Demonstrates configuring Serilog sinks, enrichers, and minimum log levels entirely through `appsettings.json` — no code-based configuration needed |
| `LoggingScopeExample1` | Introduces `ILogger.BeginScope()` to attach contextual properties (e.g. request ID, user ID) to every log entry within a scope block |
| `LoggingScopeExample2` | Shows a more advanced logging scope pattern with structured scope state objects |
| `VibeCodeSerilogAspNetCore` | A vibe-coded ASP.NET Core Web API with full Serilog integration — generated with AI assistance to show real-world structured logging setup |

## 🚀 Getting Started

1. Clone the repository
2. Open `StructuredLoggingExamples.slnx` in Visual Studio 2022+ or use `dotnet run`
3. Run any project: `dotnet run --project AspNetCoreSerilogFromAppSettingsExample`
4. Observe structured log output in the console with enriched properties

## 🔑 Key Concepts

- **Structured Logging** — logs as queryable key-value pairs instead of flat strings, enabling filtering and aggregation in tools like Seq or Application Insights
- **Serilog** — the most popular structured logging library for .NET with a rich sink ecosystem
- **Logging Scopes** — `ILogger.BeginScope()` attaches ambient context (correlation IDs, user info) to all log entries within a code block
- **AppSettings Configuration** — configure Serilog sinks, enrichers, and levels via `appsettings.json` for environment-specific logging without redeployment

## 🔗 Connect with Dev Leader

- 🎥 **YouTube:** [youtube.com/@devleader](https://www.youtube.com/@devleader)
- 📝 **Blog:** [devleader.ca](https://www.devleader.ca)
- 🌐 **All Links:** [linktr.ee/devleader](https://www.linktr.ee/devleader)
