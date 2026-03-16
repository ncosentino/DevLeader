# RefactoringCompositionDITesting - Refactoring with Composition, Dependency Injection, and Testing

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution demonstrates a progressive refactoring journey from a monolithic class design to a well-composed, dependency-injected, and testable architecture. It walks through three stages of the same URL-saving application, showing how composition and dependency injection transform tightly coupled code into maintainable, unit-testable software.

The **StartingPoint** project contains a monolithic `AwesomeUrlSaver` class that mixes validation, HTTP downloading, HTML parsing, formatting, and file I/O into a single untestable class. The **CompositionRefactor1** project breaks this into five focused classes — `UrlNormalizer`, `HtmlContentDownloader`, `HtmlUrlExtractor`, `UrlListOutputFormatter`, and `UrlContentFileWriter` — with `AwesomeUrlSaver` becoming an orchestrator that accepts dependencies via constructor injection. The **CompositionRefactor1.Autofac** project takes this further by using the Autofac IoC container to manage object lifecycle and automate dependency wiring. The **UrlParsingUnitTests** project provides xUnit tests demonstrating how the refactored design enables isolated unit testing.

This progression clearly illustrates how the Single Responsibility Principle, composition over inheritance, and dependency injection work together to produce testable, maintainable code.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the starting point (monolithic version)
dotnet run --project RefactoringCompositionDITesting.StartingPoint

# Run the composition-refactored version
dotnet run --project RefactoringCompositionDITesting.CompositionRefactor1

# Run the Autofac DI version
dotnet run --project RefactoringCompositionDITesting.CompositionRefactor1.Autofac

# Run the unit tests
dotnet test --project RefactoringCompositionDITesting.UrlParsingUnitTests
```

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
