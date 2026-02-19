# Microsoft Testing Platform Examples — xUnit v3, TUnit, MSTest, and Legacy xUnit

[![Dev Leader YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Dev Leader Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Linktree](https://img.shields.io/badge/Linktree-devleader-green?logo=linktree)](https://www.linktr.ee/devleader)

Explore the **Microsoft Testing Platform (MTP)** — the modern, lightweight replacement for VSTest — with practical examples across the most popular .NET test frameworks: **xUnit v3**, **TUnit**, **MSTest**, and legacy **xUnit v2**. Each project shows how to wire up the framework with MTP, run tests as standalone executables, and take advantage of the new extensibility model.

## 📺 Watch the Videos

### Microsoft Testing Platform CHANGES EVERYTHING for Your Tests!
[![Microsoft Testing Platform CHANGES EVERYTHING for Your Tests!](https://img.youtube.com/vi/pYfT05L8C7o/hqdefault.jpg)](https://youtu.be/pYfT05L8C7o)

### Write Better C# Tests with xUnit V3 and Microsoft Testing Platform
[![Write Better C# Tests with xUnit V3 and Microsoft Testing Platform](https://img.youtube.com/vi/Y1Haso1JrfA/hqdefault.jpg)](https://youtu.be/Y1Haso1JrfA)

### A Beginner's Guide to TUnit and Microsoft Testing Platform
[![A Beginner's Guide to TUnit and Microsoft Testing Platform](https://img.youtube.com/vi/Vaz2zLWrta0/hqdefault.jpg)](https://youtu.be/Vaz2zLWrta0)

### Beginner's Guide For Writing TUnit Tests In C#
[![Beginner's Guide For Writing TUnit Tests In C#](https://img.youtube.com/vi/w4ImKF9KsNE/hqdefault.jpg)](https://youtu.be/w4ImKF9KsNE)

## 🗂️ Projects

| Project | Framework | Description |
|---------|-----------|-------------|
| `MicrosoftTestingPlatformExamples.SystemUnderTestProject` | — | The production code being tested across all test projects |
| `MicrosoftTestingPlatformExamples.TestsWithMSTest` | MSTest | Tests using MSTest with native Microsoft Testing Platform support |
| `MicrosoftTestingPlatformExamples.XUnit` | xUnit v3 | Tests using xUnit v3 with first-class MTP integration and standalone executable output |
| `MicrosoftTestingPlatformExamples.LegacyXUnit` | xUnit v2 (legacy) | Tests demonstrating xUnit v2 compatibility mode under MTP |
| `MicrosoftTestingPlatformExamples.TUnit` | TUnit | Tests using TUnit, a framework built entirely on Microsoft Testing Platform |

## 🚀 Getting Started

1. Clone the repository
2. Open `MicrosoftTestingPlatformExamples.sln` in Visual Studio 2022+ (or use `dotnet test`)
3. Run all tests: `dotnet test`
4. Run a single test project as a standalone executable: `dotnet run --project MicrosoftTestingPlatformExamples.XUnit`

## 🔑 Key Concepts

- **Microsoft Testing Platform** — a lightweight, AOT-compatible test runner replacing VSTest; test projects compile as self-contained executables
- **xUnit v3** — the latest xUnit release with native MTP support and improved parallelism
- **TUnit** — a new .NET test framework built from the ground up on Microsoft Testing Platform with source-generated test discovery
- **Legacy xUnit (v2)** — demonstrates backward-compatible usage of older xUnit projects under MTP

## 🔗 Connect with Dev Leader

- 🎥 **YouTube:** [youtube.com/@devleader](https://www.youtube.com/@devleader)
- 📝 **Blog:** [devleader.ca](https://www.devleader.ca)
- 🌐 **All Links:** [linktr.ee/devleader](https://www.linktr.ee/devleader)
