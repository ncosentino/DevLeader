# Plugin & Modular Architecture in C# – CYC 2025 Resources & Examples

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This page is a curated landing hub for plugin- or modular-architecture resources in C#, authored by **Dev Leader / Nick Cosentino**. The goal is to help you jump into the world of plugin systems with concrete examples, patterns, and library tools.

---

## 🎤 Slides & Presentations

- **DotNet Plugin Architecture – CYC 2025 Talk**  
  [Download the slides](https://github.com/ncosentino/DevLeader/blob/master/CYC2025/DotNet%20Plugin%20Architecture%20-%20CYC2025.pptx)  
  Covers:
  - Kernel vs Feature Plugins  
  - Dependency Injection & Assembly Scanning  
  - SDK design considerations  
  - Testing plugins in isolation  
  - Needlr (Scrutor + Autofac-style library)

---

## 🎥 Selected YouTube Videos

| Title | Description / Key Topics |
|---|---|
| [How I Made C# Plugins Super Simple in My DotNet Apps!](https://www.youtube.com/watch?v=IH5HEkcMsMg) | Walkthrough of using **Needlr** for plugin-style design. |
| [Minimizing Boilerplate C# By Refactoring With Needlr](https://www.youtube.com/watch?v=E9zAw6OJA6Q) | Refactoring to reduce DI boilerplate, showing how plugin setup becomes easier. |
| [Plugin Architecture in C# – Principal Software Engineering AMA](https://www.youtube.com/watch?v=teNAo9tDSYA) | Your perspectives on plugin-related architecture questions. |

👉 More videos: [Dev Leader YouTube Channel](https://www.youtube.com/@DevLeader/videos)  

---

## 📝 Blog Articles & Tutorials

| Title | Summary / Why It Matters |
|---|----------------------------|
| [Plugin Architecture in C# for Improved Software Design](https://www.devleader.ca/2024/03/12/plugin-architecture-in-c-for-improved-software-design) | Intro + patterns for loading, discovery, and managing plugin modules. |
| [Plugin Architectures in DotNet (Dev Leader Weekly #54)](https://stories.devleader.ca/plugin-architectures-in-dotnet-4dfa9445eb98) | Reflection, DI, and the tradeoffs when building plugin systems. |
| [Plugin Architecture in ASP.NET Core – How To Master It](https://www.devleader.ca/2023/07/31/plugin-architecture-in-aspnet-core-how-to-master-it) | Practical walkthrough for plugin support in ASP.NET Core, including Autofac. |
| [Blazor Plugin Architecture – How To Manage Dynamic Loading & Lifecycle](https://www.devleader.ca/2023/09/15/blazor-plugin-architecture-how-to-manage-dynamic-loading-lifecycle) | Applying modularity in Blazor apps, lifecycle, and DI. |
| [Blazor Plugin Architecture – A How To Guide](https://www.devleader.ca/2023/09/14/plugin-architecture-in-blazor-a-how-to-guide) | Step-by-step guide for plugin support in Blazor + Autofac. |
| [Plugin Architecture Design Pattern — A Beginner’s Guide to Modularity](https://medium.devleader.ca/plugin-architecture-design-pattern-a-beginners-guide-to-modularity-2ff88e2a55d5) | High-level overview of plugin pattern concepts and rationale. |

---

## 🧰 Tools, Libraries & Key Concepts

- **[Needlr](https://github.com/ncosentino/needlr)** – DI scanning and plugin registration library.  
- **Autofac** – Popular IoC container, strong for modular organization.  
- **Scrutor** – Assembly scanning extensions for Microsoft.Extensions.DependencyInjection.  
- **Plugin SDK / Contracts** – Interfaces for communication between host and plugins.  
- **Assembly Scanning** – Discover and register plugin types dynamically.  
- **Testing** – Feature plugins test in isolation; kernel plugins require integration coverage.  

---

## 🚀 Getting Started

1. Watch the **videos** to see plugin architectures in action.  
2. Read the **blog posts** to dive into design patterns, trade-offs, and code samples.  
3. Explore **Needlr** and wire up a minimal plugin project.  
4. Start small:  
   - Define an `IPlugin` contract.  
   - Load a plugin DLL at runtime.  
   - Register via DI container.  
   - Resolve and invoke `IEnumerable<IPlugin>`.  
5. Grow incrementally: Add plugin metadata, lifecycle hooks, and hot reload.

---

NOTE: after CYC2025, I will have a live stream and add that content to here as well.

---

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
