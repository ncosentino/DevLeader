# StreamingData.DownloadVideoExample - HTTP Streaming and Download Patterns in C# .NET

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This educational project demonstrates efficient HTTP streaming patterns for downloading large files in C# .NET. It compares the `ResponseHeadersRead` and `ResponseContentRead` completion strategies when using `HttpClient`, showing how streaming can reduce memory usage and improve responsiveness for large downloads.

The solution includes three projects: a console application for interactive demos, a class library containing the core `StreamDownloader` class and a custom `StreamWithLength` decorator, and a BenchmarkDotNet benchmarks project for measuring performance differences between the two approaches. The `StreamWithLength` decorator wraps a stream to expose content length metadata while preserving streaming behavior.

Built on .NET 8, this project serves as a practical reference for developers working with HTTP downloads, streaming I/O, and performance benchmarking in C#.

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Internet connection (downloads sample video)

### Running the Project
```bash
# Run the console demo
cd StreamingData.DownloadVideoExample
dotnet run

# Run the benchmarks
cd StreamingData.Benchmarks
dotnet run -c Release
```

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
