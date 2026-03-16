# BlazorPluginsExample - Plugin Architecture for Blazor with Dynamic UI Component Loading

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This multi-project solution demonstrates how to build a plugin architecture for Blazor Server applications. It uses Autofac for dependency injection and dynamic assembly loading to discover and register plugin components at runtime. Plugins are loaded by scanning the application's base directory for DLLs matching a naming convention (`*plugin*.dll`), allowing new UI components to be added without modifying the host application.

The solution includes a Plugin API project that defines the contracts plugins must implement, a Navigation Plugin API for navigation-aware plugins, and several example plugin libraries including HTML fragment plugins and render fragment plugins. The `NavigationModule` uses Autofac to scan loaded assemblies for types implementing `INavigationPlugin` and registers them through a generic plugin provider. This architecture enables truly extensible Blazor applications where third-party or team-developed UI components can be dropped in as DLLs.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- NuGet packages:
  - `Autofac.Extensions.DependencyInjection` v8.0.0
  - `Microsoft.AspNetCore.Components.Web` v7.0.11

### Running the Project
```bash
cd BlazorPluginsExample
dotnet run
```

Navigate to `http://localhost:<port>/` to see the dynamically loaded plugin components.

## Related Resources

### Blog Articles
- [Blazor Plugin Architecture - How to Dynamically Load UI Components](https://www.devleader.ca/2024/08/08/blazor-plugin-architecture-how-to-dynamically-load-ui-components) - Explains the full plugin architecture pattern for Blazor applications

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
