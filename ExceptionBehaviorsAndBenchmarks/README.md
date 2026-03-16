# ExceptionBehaviorsAndBenchmarks - C# Exception Handling Performance Benchmarks with BenchmarkDotNet

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project uses BenchmarkDotNet to measure and compare the performance characteristics of different exception handling patterns in C# .NET 7.0. It includes three benchmark suites: `ThrowVsReturnBenchmarks` comparing implicit rethrow, explicit rethrow, and non-throwing tuple return approaches across a 5-method call stack; `ThrowRethrowBenchmarks` comparing implicit `throw` versus explicit `throw ex` rethrow strategies; and `TryCatchBenchmarks` testing various try-catch patterns including static versus new exception allocation, catch with and without variable capture, and catching all exceptions versus specific exception types.

These benchmarks help developers understand the real performance cost of exception handling in .NET, including the overhead of exception propagation through call stacks, the impact of different rethrow strategies, and whether returning errors as values is more efficient than throwing exceptions. The project uses BenchmarkDotNet's `[ShortRunJob]` attribute for quick iteration and provides meaningful, comparable results across all tested patterns.

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- Run in Release mode for accurate benchmarks

### Running the Project
```bash
cd ExceptionBehaviorsAndBenchmarks
dotnet run -c Release
```

## Related Resources

### Blog Articles
- [How to Use BenchmarkDotNet: 6 Simple Performance-Boosting Tips to Get Started](https://www.devleader.ca/2024/03/05/how-to-use-benchmarkdotnet-simple-performance-boosting-tips-to-get-started/)

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
