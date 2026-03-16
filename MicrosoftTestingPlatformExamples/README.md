# Microsoft Testing Platform Examples — xUnit v3, TUnit, MSTest, and .NET Test Runner

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

Explore the **Microsoft Testing Platform (MTP)** — the modern, lightweight replacement for VSTest — with practical examples across the most popular .NET test frameworks: **xUnit v3**, **TUnit**, **MSTest**, and legacy **xUnit v2**. Each project shows how to wire up the framework with MTP, run tests as standalone executables, and take advantage of the new extensibility model.

## Watch the Videos

### Microsoft Testing Platform CHANGES EVERYTHING for Your Tests!
[![Microsoft Testing Platform CHANGES EVERYTHING for Your Tests!](https://img.youtube.com/vi/pYfT05L8C7o/hqdefault.jpg)](https://youtu.be/pYfT05L8C7o)

### Write Better C# Tests with xUnit V3 and Microsoft Testing Platform
[![Write Better C# Tests with xUnit V3 and Microsoft Testing Platform](https://img.youtube.com/vi/Y1Haso1JrfA/hqdefault.jpg)](https://youtu.be/Y1Haso1JrfA)

### A Beginner's Guide to TUnit and Microsoft Testing Platform
[![A Beginner's Guide to TUnit and Microsoft Testing Platform](https://img.youtube.com/vi/Vaz2zLWrta0/hqdefault.jpg)](https://youtu.be/Vaz2zLWrta0)

### Beginner's Guide For Writing TUnit Tests In C#
[![Beginner's Guide For Writing TUnit Tests In C#](https://img.youtube.com/vi/w4ImKF9KsNE/hqdefault.jpg)](https://youtu.be/w4ImKF9KsNE)

## Projects

| Project | Framework | Description |
|---------|-----------|-------------|
| `MicrosoftTestingPlatformExamples.SystemUnderTestProject` | — | The production code being tested across all test projects |
| `MicrosoftTestingPlatformExamples.TestsWithMSTest` | MSTest | Tests using MSTest with native Microsoft Testing Platform support |
| `MicrosoftTestingPlatformExamples.XUnit` | xUnit v3 | Tests using xUnit v3 with first-class MTP integration and standalone executable output |
| `MicrosoftTestingPlatformExamples.LegacyXUnit` | xUnit v2 (legacy) | Tests demonstrating xUnit v2 compatibility mode under MTP |
| `MicrosoftTestingPlatformExamples.TUnit` | TUnit | Tests using TUnit, a framework built entirely on Microsoft Testing Platform |

## Getting Started

1. Clone the repository
2. Open `MicrosoftTestingPlatformExamples.sln` in Visual Studio 2022+ (or use `dotnet test`)
3. Run all tests: `dotnet test`
4. Run a single test project as a standalone executable: `dotnet run --project MicrosoftTestingPlatformExamples.XUnit`

## Key Concepts

- **Microsoft Testing Platform** — a lightweight, AOT-compatible test runner replacing VSTest; test projects compile as self-contained executables
- **xUnit v3** — the latest xUnit release with native MTP support and improved parallelism
- **TUnit** — a new .NET test framework built from the ground up on Microsoft Testing Platform with source-generated test discovery
- **Legacy xUnit (v2)** — demonstrates backward-compatible usage of older xUnit projects under MTP

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
