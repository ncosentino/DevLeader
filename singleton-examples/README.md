# singleton-examples - Singleton Pattern Implementations in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates two implementations of the Singleton design pattern in C#, highlighting the critical importance of thread safety when managing single-instance objects in multi-threaded applications. It provides a clear comparison between a naive approach and a properly synchronized implementation.

The **ThreadSafeSingleton** class uses the double-checked locking pattern with a private lock object for correct thread-safe lazy initialization. It performs two null checks — one outside the lock for performance (avoiding lock acquisition on subsequent calls) and one inside the lock for correctness (preventing race conditions). The **NotThreadSafeSingleton** class uses a simple single null check without any synchronization, demonstrating how concurrent access can create multiple instances and violate the singleton guarantee. Both classes use private constructors to prevent external instantiation and expose a static `Instance` property.

This educational example teaches developers about the importance of thread synchronization in singleton patterns and helps them understand the risks of using non-thread-safe implementations in production code.

## Getting Started

### Prerequisites
- .NET Framework 2.0 or later
- An IDE such as Visual Studio

### Running the Project
Open the solution in Visual Studio and run the console application.

## Related Resources

### Blog Articles
- [Singleton Design Pattern in C#: Complete Guide with Examples](https://www.devleader.ca/2026/03/15/singleton-design-pattern-in-c-complete-guide-with-examples)

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
