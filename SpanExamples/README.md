# SpanExamples - Span&lt;T&gt; Performance Benchmarks in C# .NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project benchmarks `Span<T>` slicing versus traditional `string.Substring()` operations using BenchmarkDotNet in C# .NET. It demonstrates how `Span<T>` enables zero-allocation memory access patterns for text processing, avoiding heap allocations when scanning text for empty lines.

The benchmarks test performance across multiple character sizes — 1K, 10K, and 100K characters — to show how `Span<T>` scales compared to `Substring`. By leveraging stack-allocated spans, the code avoids creating intermediate string objects on the managed heap, which can significantly reduce GC pressure in hot paths.

Built on .NET 7 with BenchmarkDotNet 0.13.5, this project serves as a practical reference for developers looking to understand when and how to use `Span<T>` for performance-sensitive string and memory operations in C#.

## Getting Started

### Prerequisites
- .NET 7.0 SDK

### Running the Project
```bash
cd SpanExamles\SpanExamles.Benchmarks
dotnet run -c Release
```

## Related Resources
- [Span&lt;T&gt; in C# - What Every .NET Developer Needs to Know](https://www.devleader.ca/2024/04/22/spant-in-c-what-every-net-developer-needs-to-know)

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
