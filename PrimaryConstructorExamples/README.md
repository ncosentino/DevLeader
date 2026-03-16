# PrimaryConstructorExamples - C# Primary Constructors

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates the C# 12 primary constructor syntax for classes, comparing it side-by-side with record types and traditional constructors. It provides a clear, concise example of how primary constructors reduce boilerplate code while maintaining the same functionality.

The code showcases three approaches: **record types** (`MyRecord`) with their concise immutable syntax, **classic constructor classes** (`ClassicConstructorClass`) with explicit init-only properties and manual constructor assignment, and **primary constructor classes** (`PrimaryConstructorClass`) where constructor parameters become directly accessible private fields within the class. This comparison highlights how primary constructors eliminate the need for explicit field declarations and constructor bodies, making them especially useful for dependency injection patterns and simple data-holding classes.

Whether you are adopting C# 12 features or evaluating when to use records versus classes, this example provides a practical reference for understanding the new primary constructor syntax and its trade-offs.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later (C# 12 support required)
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
dotnet run --project PrimaryConstructorExamples
```

## Related Resources

### Blog Articles
- [Primary Constructor in C# - What You Need to Know](https://www.devleader.ca/2024/02/11/primary-constructor-in-c-what-you-need-to-know)

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
