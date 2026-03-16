# WorkingWithDictionaries - Dictionary Usage and Hash Code Patterns in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

Educational console application demonstrating dictionary usage in C#, specifically how to convert arrays to dictionaries using LINQ's ToDictionary() method and how hash code collisions affect dictionary performance. Features a custom Person record type with an intentionally simplistic GetHashCode implementation (returns only Age) to demonstrate how hash collisions reduce dictionary performance by converting hash buckets into linked lists.

Built on .NET 7 with no external dependencies, this project provides a hands-on way to understand the relationship between hash codes and dictionary behavior. By observing what happens when multiple keys produce the same hash code, developers gain insight into why a well-distributed GetHashCode implementation is critical for dictionary performance.

## Getting Started

### Prerequisites
- .NET 7.0 SDK

### Running the Project
```bash
cd WorkingWithDictionaries\WorkingWithDictionaries
dotnet run
```

## Related Resources

- [How to Use a Dictionary in C#](https://www.devleader.ca/2024/01/27/how-to-use-a-dictionary-in-c)

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
