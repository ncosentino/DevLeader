# LegacyCodeNightmare - Refactoring Legacy C# Code with Dependency Injection and Testing

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This .NET 7.0 solution demonstrates how to incrementally refactor legacy code by introducing dependency injection, abstractions, and async patterns while maintaining backward compatibility and testability. The project implements a blog fetching and processing system that retrieves HTML content from a URL, saves it locally, processes it, and writes timestamped output files. The code intentionally showcases common legacy anti-patterns alongside their refactored counterparts.

Key refactoring improvements demonstrated include extracting hard-coded HTTP calls into an `IHttpClient` interface with a `HttpClientWrapper` implementation, converting synchronous blocking `.GetAwaiter().GetResult()` calls to proper async/await, and separating file I/O concerns into a dedicated `BlogFetcher` class. The solution includes an xUnit test project using Moq for mocking the `IHttpClient` dependency, showing how dependency injection enables testability in previously untestable code.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later

### Running the Project
```bash
cd LegacyCodeNightmare/LegacyCodeNightmare.SomeLegacyCode
dotnet run
```

## Related Resources

### Blog Articles
- [Refactoring Legacy Code - What You Need To Be Effective](https://www.devleader.ca/2023/11/27/refactoring-legacy-code-what-you-need-to-be-effective)

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
