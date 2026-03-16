# RedisExample - Redis Caching in .NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution demonstrates two practical approaches to Redis caching in .NET 9 — a console application for basic key-value operations and an ASP.NET Core Web API with a production-style caching pattern. Together, they provide a hands-on introduction to integrating Redis into .NET applications using StackExchange.Redis.

The **RedisExample** console app connects to a local Redis instance and provides an interactive loop for setting and retrieving string key-value pairs using `StackExchange.Redis`. The **RedisExample.WebApi** project demonstrates a cache-aside pattern in an ASP.NET Core minimal API, where a `GET /weatherforecast` endpoint checks Redis for cached data before generating a new forecast. It uses `StackExchange.Redis.Extensions.AspNetCore` with System.Text.Json serialization for dependency injection-friendly Redis integration, and includes console logging to show cache hits versus misses.

These examples are ideal for developers learning how to add distributed caching to .NET applications for improved performance and scalability.

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- A running Redis server (e.g., via Docker: `docker run -p 6379:6379 redis`)
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the console app
dotnet run --project RedisExample

# Run the Web API
dotnet run --project RedisExample.WebApi
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
