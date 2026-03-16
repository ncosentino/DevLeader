# OurWebApi - ASP.NET Core Web API with Dapper, DbUp, and SQLite

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates building a minimal ASP.NET Core Web API using Dapper for lightweight data access, DbUp for automated database migrations, and SQLite as the backing database. It showcases a practical, production-inspired approach to building data-driven APIs without heavy ORM frameworks.

The application starts by running embedded SQL migration scripts via DbUp to create and seed a `WeatherForecast` table in SQLite. A single `GET /weatherforecast` endpoint accepts a `minimumDateTimeUtc` query parameter and uses Dapper to execute a parameterized SQL query, returning filtered weather forecast data. The entire application is built using .NET 8 minimal API conventions with records for data models, async/await patterns, and nullable reference types.

This is a clean educational example showing how to combine lightweight tools for rapid API development — ideal for learning core concepts like database access, schema versioning, and parameterized queries without the overhead of Entity Framework.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider
- No external database setup required (SQLite is file-based)

### Running the Project
```bash
dotnet run --project OurWebApi
```
Then use the included `.http` file or navigate to `https://localhost:<port>/weatherforecast` to test the endpoint.

## Related Resources

### YouTube Videos
- [Let's Build A Web API - ASP.NET Core, Dapper, DbUp, and SQLite](https://www.youtube.com/watch?v=YNhhcRLjKDM)

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
