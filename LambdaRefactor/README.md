# LambdaRefactor - Refactoring C# Code with Lambda Expressions to Reduce Boilerplate

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This .NET Framework 2.0 console application demonstrates how lambda expressions can be used to refactor a processor factory pattern, reducing code duplication and improving maintainability. The project contains parallel pre-refactor and post-refactor implementations: the pre-refactor approach uses separate concrete processor classes like `GreaterProcessor` and `StringEqualsProcessor` with hardcoded comparison logic, while the post-refactor approach uses generic `NumericProcessor<T>` and `StringProcessor<T>` classes that accept comparison logic as lambda expressions via delegates.

The key insight is that the post-refactor approach replaces the need to create new classes for each processor type with a single lambda expression passed to a generic processor. The project includes commented-out exercises for learners to implement additional processor types in both styles to understand the complexity trade-offs firsthand. This is a practical teaching resource for understanding when and how to apply lambda expressions in real-world C# refactoring.

## Getting Started

### Prerequisites
- .NET Framework 2.0 or later
- Visual Studio

### Running the Project
Open the solution in Visual Studio and run the console application.

## Related Resources

### Blog Articles
- [Lambdas: An Example in Refactoring Code](https://www.devleader.ca/2013/11/14/lambdas-example-refactoring-code)

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
