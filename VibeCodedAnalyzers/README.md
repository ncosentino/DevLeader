# Vibe Coded Roslyn Analyzers and Code Fixes for C# — Built with AI

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

Learn how to build custom **Roslyn analyzers and code fixes** for C# using vibe coding with AI tools like GitHub Copilot and ChatGPT. This solution demonstrates how to enforce coding conventions (such as Arrange/Act/Assert comments in tests and required assert messages) directly inside Visual Studio and the .NET compiler pipeline.

## Watch the Series

### How To Vibe Code Rules To Keep Your Coding Agents On Track
[![How To Vibe Code Rules To Keep Your Coding Agents On Track](https://img.youtube.com/vi/x9rkxzrJuFA/hqdefault.jpg)](https://youtu.be/x9rkxzrJuFA)

### Vibe Coding C# Code Fixup Solutions For Our Roslyn Analyzer
[![Vibe Coding C# Code Fixup Solutions For Our Roslyn Analyzer](https://img.youtube.com/vi/VkkpyYPOv9w/hqdefault.jpg)](https://youtu.be/VkkpyYPOv9w)

### The Secret Trick To Keep Copilot On Track With Your C# Code
[![The Secret Trick To Keep Copilot On Track With Your C# Code](https://img.youtube.com/vi/_XFxPm7YQ7U/hqdefault.jpg)](https://youtu.be/_XFxPm7YQ7U)

## Related Articles

- [Vibe Coding - Dev Leader Weekly 88](https://www.devleader.ca/2025/03/22/vibe-coding-dev-leader-weekly-88)
- [Vibe Coding 2.0 - Dev Leader Weekly 115](https://www.devleader.ca/2025/11/08/vibe-coding-20-dev-leader-weekly-115)

## Projects

| Project | Description |
|---------|-------------|
| `VibeCodedAnalyzers.Analyzers` | Roslyn analyzer rules: `ArrangeActAssertCommentAnalyzer` enforces AAA comments in test methods; `AssertMessageAnalyzer` requires descriptive messages on xUnit assert calls |
| `VibeCodedAnalyzers.Analyzers.Tests` | Unit tests for the analyzer rules using the Roslyn testing helpers |
| `VibeCodedAnalyzers.CodeFixes` | Code fix providers that automatically correct violations detected by the analyzers |
| `VibeCodedAnalyzers.ConsoleApp` | Sample C# console app that the analyzers run against |
| `VibeCodedAnalyzers.ConsoleApp.Tests` | xUnit tests for the console app — used to demonstrate the analyzers and code fixes in action |

## Getting Started

1. Clone the repository
2. Open `VibeCodedAnalyzers.slnx` in Visual Studio 2022+
3. Build the solution — the analyzers will be compiled and loaded
4. Open any test file in `VibeCodedAnalyzers.ConsoleApp.Tests` to see live diagnostics
5. Use the lightbulb quick actions to apply code fixes automatically

## Key Concepts

- **Roslyn Analyzers** — compile-time code analysis integrated into the .NET build pipeline
- **Code Fix Providers** — automatic refactoring suggestions surfaced as Visual Studio quick actions
- **Vibe Coding** — using AI (GitHub Copilot, ChatGPT) to rapidly generate and iterate on analyzer code
- **Arrange/Act/Assert** — the AAA pattern enforced as structured comments in test methods

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
