# ObserverPatternExamples - Observer Design Pattern in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution demonstrates three different implementations of the Observer design pattern in C# and .NET, showing the evolution from a traditional interface-based approach to modern reactive programming. Each project in the solution tackles the same problem — notifying subscribers of events — using a progressively more sophisticated technique.

The first project, ApiBasedObserverExample, implements the classic Gang of Four Observer pattern with manual subscription management using `IObserver` and `Observable` classes. The second project, EventBasedObserverExample, leverages C#'s native event system with delegates and `EventHandler<T>` for a more idiomatic approach. The third project, RxNetObserverExample, uses Reactive Extensions (Rx.NET) with `IObservable<T>` and `IObserver<T>` interfaces, enabling composable, scalable reactive streams.

Together, these examples provide a comprehensive comparison of observer pattern strategies in .NET, helping developers understand the trade-offs between simplicity, language integration, and scalability when implementing event-driven architectures.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the classic API-based observer example
dotnet run --project ApiBasedObserverExample

# Run the event-based observer example
dotnet run --project EventBasedObserverExample

# Run the Rx.NET observer example
dotnet run --project RxNetObserverExample
```

## Related Resources

### Blog Articles
- [Observer Design Pattern in C# - How to Simplify Event-Driven Programming](https://www.devleader.ca/2024/02/22/observer-design-pattern-in-c-how-to-simplify-event-driven-programming)

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
