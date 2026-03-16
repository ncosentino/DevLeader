# RefactorStaticSingletonDI - Refactoring from Static Singleton to Dependency Injection

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates the problems with static singleton patterns in C# and serves as a starting point for refactoring toward proper dependency injection using Autofac. It illustrates common anti-patterns found in legacy codebases — global static state, tightly coupled singletons, and implicit dependencies — that make code difficult to test and maintain.

The code features a `Globals` static class with hardcoded configuration values, an `ApiClientConfigSingleton` using `Lazy<T>` for static singleton initialization, a `MyApiClient` tightly coupled to the singleton instance, and `MyBusinessLogic` that creates `MyApiClient` directly with `new()`. These anti-patterns create a chain of hidden dependencies that prevent unit testing, make configuration inflexible, and tie the entire application to global state. The Autofac NuGet package is included to facilitate the refactoring journey toward constructor injection, interface-based abstractions, and container-managed lifetimes.

This educational project is ideal for developers learning why static singletons are problematic and how dependency injection provides a more testable, maintainable alternative.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
dotnet run --project RefactorStaticSingletonDI
```

## Related Resources

### Blog Articles
- [Singleton Design Pattern in C#: Complete Guide with Examples](https://www.devleader.ca/2026/03/15/singleton-design-pattern-in-c-complete-guide-with-examples)

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
