# Structured Logging Examples in C# — Serilog, Logging Scopes, and ASP.NET Core

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

Practical **structured logging** examples for .NET and ASP.NET Core using **Serilog**. Learn how to move beyond plain-text logs with queryable key-value properties, how to use **logging scopes** to add contextual information across multiple log entries, and how to configure Serilog entirely from `appsettings.json` without touching code.

## Watch the Videos

### STOP Passing State For Logging: A Practical Example of Logging Scopes
[![STOP Passing State For Logging: A Practical Example of Logging Scopes](https://img.youtube.com/vi/tJ8mouFN0Lk/hqdefault.jpg)](https://youtu.be/tJ8mouFN0Lk)

### Beginner's Guide To Structured Logging in C#: Logging Scopes
[![Beginner's Guide To Structured Logging in C#: Logging Scopes](https://img.youtube.com/vi/RZyO54mfQKQ/hqdefault.jpg)](https://youtu.be/RZyO54mfQKQ)

### How To Configure Serilog From AppSettings.json
[![How To Configure Serilog From AppSettings.json](https://img.youtube.com/vi/zMPqMvo7F98/hqdefault.jpg)](https://youtu.be/zMPqMvo7F98)

### Let's VIBE! ChatGPT vs Copilot — Which Integrates Serilog Better?
[![Let's VIBE! ChatGPT vs Copilot — Which Integrates Serilog Better?](https://img.youtube.com/vi/vKepCbmuBfo/hqdefault.jpg)](https://youtu.be/vKepCbmuBfo)

## Projects

| Project | Description |
|---------|-------------|
| `AspNetCoreSerilogFromAppSettingsExample` | Demonstrates configuring Serilog sinks, enrichers, and minimum log levels entirely through `appsettings.json` — no code-based configuration needed |
| `LoggingScopeExample1` | Introduces `ILogger.BeginScope()` to attach contextual properties (e.g. request ID, user ID) to every log entry within a scope block |
| `LoggingScopeExample2` | Shows a more advanced logging scope pattern with structured scope state objects |
| `VibeCodeSerilogAspNetCore` | A vibe-coded ASP.NET Core Web API with full Serilog integration — generated with AI assistance to show real-world structured logging setup |

## Getting Started

1. Clone the repository
2. Open `StructuredLoggingExamples.slnx` in Visual Studio 2022+ or use `dotnet run`
3. Run any project: `dotnet run --project AspNetCoreSerilogFromAppSettingsExample`
4. Observe structured log output in the console with enriched properties

## Key Concepts

- **Structured Logging** — logs as queryable key-value pairs instead of flat strings, enabling filtering and aggregation in tools like Seq or Application Insights
- **Serilog** — the most popular structured logging library for .NET with a rich sink ecosystem
- **Logging Scopes** — `ILogger.BeginScope()` attaches ambient context (correlation IDs, user info) to all log entries within a code block
- **AppSettings Configuration** — configure Serilog sinks, enrichers, and levels via `appsettings.json` for environment-specific logging without redeployment

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
