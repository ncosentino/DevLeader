# EventHandlerLeak - Understanding Memory Leaks from Event Handlers in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This console application demonstrates how event handlers can cause memory leaks in .NET applications. Built on .NET Framework 2.0, it provides three interactive examples that illustrate different scenarios of event handler memory leak behavior: an instance-scope event handler that causes a leak because the publisher holds a strong reference to the subscriber, an anonymous delegate without a closure that does not cause a leak, and an anonymous delegate with a closure that subtly causes a leak by implicitly capturing the instance.

Each example uses `GC.Collect()` and finalizer checks to verify whether objects are properly garbage collected after being set to null. The project clearly shows that event subscribers remain alive as long as the publisher holds a reference through the event subscription, and that closures in anonymous delegates can create hidden strong references. This is an essential learning resource for understanding why you must always pair `+=` with `-=` when working with C# events, and how seemingly innocent lambda expressions can prevent garbage collection.

## Getting Started

### Prerequisites
- .NET Framework 2.0 or later

### Running the Project
```bash
cd EventHandlerLeak\EventHandlerLeak
dotnet run
```
Or open the solution in Visual Studio and run.

## Related Resources

### Blog Articles
- [Weak Events in C# - How to Avoid Nasty Memory Leaks](https://www.devleader.ca/2024/02/14/weak-events-in-c-how-to-avoid-nasty-memory-leaks/)

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
