# ScrutorVsAutofac - Scrutor vs Autofac Dependency Injection Comparison

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution provides a side-by-side comparison of two popular .NET dependency injection frameworks — Scrutor (built on Microsoft.Extensions.DependencyInjection) and Autofac — demonstrating assembly scanning, auto-registration, and plugin architecture patterns with both libraries.

The **ScrutorVsAutofac.DemoApp** project loads all DLL assemblies from the application directory and registers services using both frameworks. Autofac discovers and registers services via `RegisterAssemblyModules()`, while Scrutor uses its fluent `services.Scan()` API to auto-discover classes implementing interfaces. Both resolve `IMyService` from their respective containers and execute the same logic. The **AutofacPlugin1** and **ScrutorPlugin1** projects implement the shared `IMyService` interface from the **ScrutorVsAutofac.SDK** project, with Autofac using an explicit `MyModule` for registration and Scrutor relying on convention-based discovery. The **RegistrationCallbackExampleApp** project demonstrates factory registration patterns, comparing Autofac's `.Register()` lambda syntax with `IServiceCollection`'s `ServiceDescriptor` approach.

This comparative analysis helps developers choose between Scrutor's lightweight convention-based scanning and Autofac's feature-rich module system for their .NET dependency injection needs.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the main comparison demo
dotnet run --project ScrutorVsAutofac.DemoApp

# Run the registration callback example
dotnet run --project ScrutorVsAutofac.RegistrationCallbackExampleApp
```

## Related Resources

### Blog Articles
- [Scrutor vs Autofac - Which .NET DI Library Should You Use?](https://www.devleader.ca/2024/06/16/scrutor-vs-autofac-which-net-di-library-should-you-use)

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
