# Benchmarking.BenchmarkDotNet - Getting Started with BenchmarkDotNet in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution contains multiple projects demonstrating different ways to set up and run benchmarks using BenchmarkDotNet in C#. The intro project (`Benchmarking.BenchmarkDotNet.Intro`) shows a complete working example that benchmarks `List<int>.Sort()` across multiple list sizes (1,000 to 1,000,000 elements) using the `[Params]` attribute, `[MemoryDiagnoser]` for allocation tracking, and `[ShortRunJob]` for quick iteration.

Additional projects in the solution demonstrate `BenchmarkSwitcher` for selecting benchmarks at runtime, base class patterns for organizing benchmarks, and different approaches to running benchmark suites. Together these examples provide a practical introduction to performance benchmarking in .NET, covering the most common patterns you will need when measuring and comparing code performance.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- NuGet package: `BenchmarkDotNet` v0.13.5

### Running the Project
```bash
cd Benchmarking.BenchmarkDotNet/Benchmarking.BenchmarkDotNet.Intro
dotnet run -c Release
```

Note: BenchmarkDotNet requires Release configuration for accurate results.

## Related Resources

### Blog Articles
- [BenchmarkDotNet - How to Get Started Benchmarking in C#](https://www.devleader.ca/2024/02/03/benchmarkdotnet-how-to-get-started-benchmarking-in-c) - A guide to setting up and running your first benchmarks with BenchmarkDotNet

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
