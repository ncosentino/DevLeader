# MoreLinqExamples - MoreLINQ Library Examples for Batch, Zip, and Collection Operations in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This .NET 8.0 solution demonstrates the MoreLINQ library (v4.3.0), an extension library that provides additional LINQ-to-Objects methods beyond what's available in standard .NET LINQ. The console application showcases two key feature areas: batching collections into fixed-size groups using `Batch()`, and advanced zip operations including `ZipLongest()` which pads shorter sequences with default values and `ZipShortest()` which explicitly stops at the shorter sequence.

The project also includes a manual `ManualZipShortest()` implementation using `GetEnumerator()` and `yield return` to demonstrate the underlying mechanics of zip operations, and a benchmarks project for comparing MoreLINQ performance. This is a practical reference for developers who need collection manipulation operations that go beyond what standard LINQ provides.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later

### Running the Project
```bash
cd MoreLinqExamples.ConsoleApp
dotnet run
```

## Related Resources

### YouTube Videos
- [Batch Collections With MoreLINQ - How To Guide And Benchmarks](https://www.youtube.com/watch?v=BVW1aDQU7mo)

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
