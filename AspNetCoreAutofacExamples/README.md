# AspNetCoreAutofacExamples - Autofac Dependency Injection in ASP.NET Core

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This multi-project solution demonstrates several approaches to integrating Autofac with ASP.NET Core, including a plugin architecture that dynamically loads assemblies at runtime. The main project (`FullResolveWebApi`) builds an Autofac container by scanning the application's base directory for assemblies containing Autofac modules, enabling a fully modular and extensible dependency injection setup.

The solution includes a Plugin SDK that defines shared interfaces, along with example plugin implementations (`Plugin1`, `Plugin2`) that are discovered and loaded at runtime. Additional projects illustrate alternative integration patterns: `ProblematicMinimalApi` shows common pitfalls when combining Autofac with minimal APIs, and `ServiceProviderWebApi` demonstrates a more traditional service provider approach. Together these examples provide a comprehensive look at Autofac DI patterns in ASP.NET Core.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- NuGet package: `Autofac.Extensions.DependencyInjection` v9.0.0

### Running the Project
```bash
cd AspNetCoreAutofacExamples/AspNetCoreAutofacExamples.FullResolveWebApi
dotnet run
```

## Related Resources

### Blog Articles
- [Autofac with ASP.NET Core - How to Get Started Quickly](https://www.devleader.ca/2024/05/15/autofac-with-aspnet-core-how-to-get-started-quickly) - A getting started guide for integrating Autofac with ASP.NET Core
- [Decorator Pattern in C# with Autofac for Improved Software Design](https://www.devleader.ca/2024/03/09/decorator-pattern-in-c-with-autofac-for-improved-software-design) - Covers using Autofac's decorator support for clean software design

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
