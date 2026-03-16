# DapperUtcConversion - Handling UTC DateTime Conversion with Dapper in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates a common problem when using Dapper with DateTime values: database `datetime` columns do not preserve the `DateTimeKind`, so values stored as UTC come back with `Kind = Unspecified`. The solution implements a custom `SqlMapper.TypeHandler<DateTime>` called `DateTimeHandler` that automatically converts retrieved DateTime values to UTC kind using `DateTime.SpecifyKind`.

The code connects to a MySQL database, inserts a `DateTime.UtcNow` value, retrieves it through the custom type handler, and uses xUnit assertions to verify that the returned DateTime has `DateTimeKind.Utc`. It also tests `DateTimeOffset` compatibility. This is a practical pattern that any developer working with Dapper and timezone-sensitive data should be aware of, as it prevents subtle bugs caused by unspecified DateTime kinds.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- A MySQL database instance
- NuGet packages:
  - `Dapper` v2.1.35
  - `MySql.Data` v9.1.0
  - `xunit` v2.9.2

### Running the Project
```bash
cd DapperUtcConversion
dotnet run
```

You will need a MySQL connection string configured before running.

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
