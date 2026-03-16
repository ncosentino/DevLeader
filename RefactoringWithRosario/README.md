# RefactoringWithRosario - Live Refactoring Examples in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project is the companion code for a live coding refactoring session with Rosario Martone, demonstrating practical refactoring techniques focused on dependency injection and testability in C#. It shows the progression from tightly coupled code to a well-abstracted, mockable, and unit-testable design.

The initial design features `OurApiClient` and business logic classes (`OurBusinessLogic`, `RosariosBusinessLogic`) that hard-code `HttpClient` creation, making them impossible to unit test in isolation. The refactoring introduces abstraction layers including `IProxyHttpClientFactory` and `IHttpClient` interfaces, along with `ProxyFactory` and `HttpClientWrapper` implementations. Multiple constructor overloads enable flexible dependency injection. The **RefactoringWithRosario.Tests** project uses xUnit and Moq to demonstrate how the refactored code can be tested by mocking HTTP dependencies, eliminating the need for real network calls during testing.

This project teaches essential refactoring principles for moving from hard-coded dependencies to dependency injection patterns that enable proper unit testing.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the console application
dotnet run --project RefactoringWithRosario.ConsoleApp

# Run the unit tests
dotnet test --project RefactoringWithRosario.Tests
```

## Related Resources

### YouTube Videos
- [An Exercise in Refactoring - Live Coding in C#](https://www.youtube.com/watch?v=6Yu8hJoWYuU)

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
