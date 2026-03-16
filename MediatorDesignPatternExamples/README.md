# MediatorDesignPatternExamples - Mediator Pattern in C# with Manual and MediatR Implementations

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This .NET 7.0 solution demonstrates the Mediator design pattern through two parallel implementations of a chat application. The `MediatorChatConsoleApp` project provides a manual implementation where a `ChatMediator` class registers users and routes messages between them, demonstrating the core concept of decoupling communication between components through a central mediator object.

The `MediatrConsoleApp` project implements the same chat functionality using the popular MediatR NuGet library (v12.2.0) with Microsoft.Extensions.DependencyInjection. It uses `IRequest` and `IRequestHandler<T>` to dispatch `ChatMessage` requests through the mediator pipeline, showcasing an enterprise-ready approach with full async support and dependency injection integration. Comparing the two implementations side by side highlights the trade-offs between simplicity and scalability when choosing between manual and library-based mediator patterns.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later

### Running the Project
```bash
cd MediatorDesignPatternExamples/MediatorChatConsoleApp
dotnet run
```

## Related Resources

### Blog Articles
- [Mediator Pattern in C# - How to Simplify Component Communication](https://www.devleader.ca/2023/11/20/mediator-pattern-in-c-how-to-simplify-component-communication)

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
