# FusionCacheExamples - FusionCache Distributed Caching Patterns in ASP.NET Core

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This .NET 9.0 solution demonstrates multiple caching strategies using FusionCache, Microsoft's HybridCache, and IMemoryCache in ASP.NET Core web APIs. The solution contains four projects: an introductory web API showing basic `GetOrSetAsync()` patterns with fail-safe caching, a playground web API demonstrating concurrent request handling and cache stampede prevention with 1000 simultaneous requests, a repositories project implementing the decorator pattern to transparently add caching layers to CRUD operations, and a traffic spammer console utility for load testing.

The repositories project is the most comprehensive, showcasing three caching implementations (IMemoryCache, FusionCache, and HybridCache) applied as decorators over Entity Framework Core and Dapper data access layers with SQLite. It includes Redis integration for distributed caching with backplane support for cross-instance cache coherency, and System.Text.Json serialization for cache entries. This is a production-ready reference for implementing multi-level caching in .NET applications.

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Redis (optional, for distributed caching in the repositories project)

### Running the Project
```bash
cd FusionCacheExamples/FusionCacheExamples.IntroWebApi
dotnet run
```

## Related Resources

### Blog Articles
- [FusionCache in C# - How to Get Started for Beginners](https://www.devleader.ca/2024/05/14/fusioncache-in-c-how-to-get-started-for-beginners)

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
