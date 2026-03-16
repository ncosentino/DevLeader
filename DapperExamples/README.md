# DapperExamples - Getting Started with Dapper ORM in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution demonstrates using Dapper, a lightweight ORM for .NET, compared side by side with classic ADO.NET data access. The main project (`DapperExamples.SimpleDapper`) creates an in-memory SQLite database, sets up a test table, and then performs identical insert and query operations using both traditional ADO.NET (manual `SqlCommand` building with parameters) and Dapper's simplified extension methods. This makes it easy to see exactly how much boilerplate Dapper eliminates.

The example uses C# record types for the data model (`Entry` with Id, Name, and Value) and covers async operations throughout. A companion benchmarks project (`DapperExamples.Benchmarks`) is included for comparing the performance characteristics of the two approaches. Together these projects provide a practical introduction to Dapper and help you evaluate whether it fits your data access needs.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- NuGet packages:
  - `Dapper` v2.1.35
  - `System.Data.SQLite` v1.0.118

### Running the Project
```bash
cd DapperExamples/DapperExamples.SimpleDapper
dotnet run
```

## Related Resources

### Blog Articles
- [Dapper in C# - An Intro for Beginners](https://www.devleader.ca/2024/01/22/dapper-in-c-an-intro-for-beginners) - Introduction to Dapper with practical examples
- [Insert Data with Dapper in C# - How To Guide for Beginners](https://www.devleader.ca/2024/02/14/insert-data-with-dapper-in-c-how-to-guide-for-beginners) - Step-by-step guide for inserting data using Dapper

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
