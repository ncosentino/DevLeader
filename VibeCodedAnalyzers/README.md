# Vibe Coded Roslyn Analyzers — C# Code Analysis with AI

[![Dev Leader YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Dev Leader Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Linktree](https://img.shields.io/badge/Linktree-devleader-green?logo=linktree)](https://links.devleader.ca)

Learn how to build custom **Roslyn analyzers and code fixes** for C# using vibe coding with AI tools like GitHub Copilot and ChatGPT. This solution demonstrates how to enforce coding conventions (such as Arrange/Act/Assert comments in tests and required assert messages) directly inside Visual Studio and the .NET compiler pipeline.

## 📺 Watch the Series

### How To Vibe Code Rules To Keep Your Coding Agents On Track
[![How To Vibe Code Rules To Keep Your Coding Agents On Track](https://img.youtube.com/vi/x9rkxzrJuFA/hqdefault.jpg)](https://youtu.be/x9rkxzrJuFA)

### Vibe Coding C# Code Fixup Solutions For Our Roslyn Analyzer
[![Vibe Coding C# Code Fixup Solutions For Our Roslyn Analyzer](https://img.youtube.com/vi/VkkpyYPOv9w/hqdefault.jpg)](https://youtu.be/VkkpyYPOv9w)

### The Secret Trick To Keep Copilot On Track With Your C# Code
[![The Secret Trick To Keep Copilot On Track With Your C# Code](https://img.youtube.com/vi/_XFxPm7YQ7U/hqdefault.jpg)](https://youtu.be/_XFxPm7YQ7U)

## 🗂️ Projects

| Project | Description |
|---------|-------------|
| `VibeCodedAnalyzers.Analyzers` | Roslyn analyzer rules: `ArrangeActAssertCommentAnalyzer` enforces AAA comments in test methods; `AssertMessageAnalyzer` requires descriptive messages on xUnit assert calls |
| `VibeCodedAnalyzers.Analyzers.Tests` | Unit tests for the analyzer rules using the Roslyn testing helpers |
| `VibeCodedAnalyzers.CodeFixes` | Code fix providers that automatically correct violations detected by the analyzers |
| `VibeCodedAnalyzers.ConsoleApp` | Sample C# console app that the analyzers run against |
| `VibeCodedAnalyzers.ConsoleApp.Tests` | xUnit tests for the console app — used to demonstrate the analyzers and code fixes in action |

## 🚀 Getting Started

1. Clone the repository
2. Open `VibeCodedAnalyzers.slnx` in Visual Studio 2022+
3. Build the solution — the analyzers will be compiled and loaded
4. Open any test file in `VibeCodedAnalyzers.ConsoleApp.Tests` to see live diagnostics
5. Use the lightbulb (💡) quick actions to apply code fixes automatically

## 🔑 Key Concepts

- **Roslyn Analyzers** — compile-time code analysis integrated into the .NET build pipeline
- **Code Fix Providers** — automatic refactoring suggestions surfaced as Visual Studio quick actions
- **Vibe Coding** — using AI (GitHub Copilot, ChatGPT) to rapidly generate and iterate on analyzer code
- **Arrange/Act/Assert** — the AAA pattern enforced as structured comments in test methods

## 🔗 Connect with Dev Leader

- 🎥 **YouTube:** [youtube.com/@devleader](https://www.youtube.com/@devleader)
- 📝 **Blog:** [devleader.ca](https://www.devleader.ca)
- 🌐 **All Links:** [links.devleader.ca](https://links.devleader.ca)
