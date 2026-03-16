# SemanticKernel.Intro - Getting Started with Semantic Kernel in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates how to build an AI-powered chatbot using Microsoft Semantic Kernel with Azure OpenAI and custom plugins in .NET 9. It showcases the core concepts of Semantic Kernel — kernel setup, plugin registration, automatic function calling, and conversational chat history management.

The application configures a Semantic Kernel instance with the GPT-4o-mini model via Azure OpenAI, then registers two custom plugins: **YouTubeCaptionsPlugin** (fetches English captions from public YouTube videos using YoutubeExplode) and **YouTubeVideosPlugin** (retrieves video metadata from a YouTube channel by handle). With auto function choice behavior enabled, the AI can autonomously decide when to invoke these plugins to answer user questions about YouTube channels and videos. The interactive chat loop maintains conversation history and uses a custom `Response<T>` type for success/error handling with record-based immutable data structures.

This is an excellent starting point for developers who want to integrate AI capabilities into their .NET applications using Semantic Kernel's plugin architecture and function calling features.

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- An Azure OpenAI deployment with a GPT-4o-mini (or compatible) model
- Azure OpenAI endpoint URL and API key
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
Update the Azure OpenAI configuration in `Program.cs` with your endpoint, deployment name, and API key, then:
```bash
dotnet run --project SemanticKernel.Intro
```

## Related Resources

### Blog Articles
- [Semantic Kernel in C# - How to Get Started](https://www.devleader.ca/2024/06/22/semantic-kernel-in-c-how-to-get-started)

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
