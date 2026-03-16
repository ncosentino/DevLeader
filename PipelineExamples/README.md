# PipelineExamples - Pipeline Design Pattern in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution demonstrates two different implementations of the Pipeline design pattern in C#, both solving the same text analysis problem — reading user input, cleaning text, counting word frequencies, and summarizing the top results. The two approaches highlight the trade-offs between structured OOP pipelines and lightweight functional pipelines.

The **AutofacPipelineConfiguration** project implements a full object-oriented pipeline using Autofac dependency injection. It defines pipeline stages as separate classes (`PipelineSource`, `TextCleaner`, `WordCounter`, `TextSummarizer`, `PipelineSink`) with interfaces like `IPipelineStage`, `IPipelineSource<T>`, and `IPipelineSink<T>`. Stages are registered as singletons in an Autofac container, and a `MostlyAutoPipelineBuilder` with `IPrioritizedPipelineIntermediate` supports priority-ordered stage execution. The **PipelineExamples** project takes a simpler functional approach using delegates and `Task.Run().ContinueWith()` chains for pipeline flow.

Together, these projects provide a comprehensive comparison of pipeline pattern strategies — from enterprise-ready DI-based composition to lightweight async delegate chains — helping developers choose the right approach for their use case.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the Autofac DI-based pipeline
dotnet run --project AutofacPipelineConfiguration

# Run the functional delegate-based pipeline
dotnet run --project PipelineExamples
```

## Related Resources

### Blog Articles
- [How To Implement The Pipeline Design Pattern in C#](https://www.devleader.ca/2024/02/19/how-to-implement-the-pipeline-design-pattern-in-c)

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
