# AspNetAutofacDecorator - Decorator Pattern in C# with Autofac and ASP.NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates how to implement the decorator pattern in an ASP.NET application using Autofac as the dependency injection container. The decorator pattern allows you to wrap an existing service with additional behavior without modifying the original implementation, and Autofac provides first-class support for registering decorators.

The example defines an `IMessageFormatter` interface with a `StandardLogMessageFormatter` as the base implementation and a `DecoratedMessageFormatter` that wraps it to add extra functionality. Two Autofac modules handle registration: `LoggingModule` registers the base formatter and `DecoratorModule` registers the decorator. A minimal API endpoint resolves the decorated service and returns the formatted message, showing how the decorator chain works in practice.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- NuGet package: `Autofac.Extensions.DependencyInjection` v8.0.0

### Running the Project
```bash
cd AspNetAutofacDecorator
dotnet run
```

Navigate to `http://localhost:<port>/` to see the decorated message output.

## Related Resources

### Blog Articles
- [Decorator Pattern in C# with Autofac for Improved Software Design](https://www.devleader.ca/2024/03/09/decorator-pattern-in-c-with-autofac-for-improved-software-design) - Explains the decorator pattern and how to use Autofac to wire it up

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
