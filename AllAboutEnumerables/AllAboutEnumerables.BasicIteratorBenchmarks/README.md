# AllAboutEnumerables.BasicIteratorBenchmarks - Iterator vs Collection Performance Benchmarks in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project uses BenchmarkDotNet to compare the performance of lazy iterators versus eagerly materialized collections in C# and .NET. The benchmarks measure real-world throughput differences between `IEnumerable<T>` iterator pipelines and `List<T>` or array-backed collections across a variety of workloads.

Understanding when to use iterators versus materialized collections is one of the most impactful performance decisions in .NET development. This project surfaces the surprising results that emerge when you measure rather than assume — some scenarios strongly favor lazy evaluation while others reveal the hidden cost of deferred execution.

## Getting Started

### Prerequisites
- .NET SDK (see project file for target framework)

### Running the Project

```bash
dotnet run -c Release
```

BenchmarkDotNet requires Release configuration for accurate results.

## Related Resources

### Blog Articles
- [Iterator Benchmarks That Shocked With Unexpected Results](https://www.devleader.ca/2023/03/17/shocking-results-from-collection-and-iterator-benchmarks/) - Deep dive into the benchmark results and what they mean for your code

### YouTube Videos
- [Shocking Iterator Performance Benchmarks in C# dotnet](https://www.youtube.com/watch?v=G2-d7kZFlRA) - Video walkthrough of the benchmark results
- [Enumerables, Iterators, and Collections Playlist](https://www.youtube.com/watch?v=RR7Cq0iwNYo&list=PLzATctVhnsgjE3qOsbkPaC1NxXD605wOC) - Full playlist covering enumerables in depth

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
