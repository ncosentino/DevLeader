# PracticalIterationAndRecursion - Iteration vs Recursion Patterns in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project compares three different approaches to traversing a file system directory structure in C#, demonstrating the practical equivalence and trade-offs between iteration and recursion. Using a real-world file/folder hierarchy as the problem domain, each approach produces the same result through fundamentally different control flow mechanisms.

The project implements **iterative depth-first traversal** using a `Stack`, **iterative breadth-first traversal** using a `Queue`, and **recursive depth-first traversal** using recursive method calls. The `Entry` record struct holds path, name, and indentation level for each directory entry, providing clean output formatting. The recursive implementation includes helper methods like `ProcessDirectoriesRecursive` and `ProcessFilesRecursive` that demonstrate array-based recursion with index parameters.

This educational example helps developers understand when to choose iteration over recursion and vice versa, the relationship between data structures (stacks vs queues) and traversal order, and how these patterns apply to hierarchical data processing in real-world applications.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
dotnet run --project PracticalIterationAndRecursion.FileFolderExample
```

## Related Resources

### YouTube Videos
- [C# Recursion With File Folder Hierarchies: Beginner's Guide](https://www.youtube.com/watch?v=3poGXDOnjo0)

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
