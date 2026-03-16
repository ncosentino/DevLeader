# Strangler Fig Pattern in C# - Incremental Monolith to Microservices Migration

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution demonstrates the **Strangler Fig Pattern** for migrating a monolithic application to microservices incrementally.

## Pattern Overview

The Strangler Fig pattern (named after the strangler fig tree that grows around existing trees) involves:
1. Starting with a monolithic application
2. Gradually extracting functionality into separate services
3. Using a facade/proxy to route requests to new services while legacy handles the rest
4. Eventually replacing the entire monolith

## Solution Structure

### Step 1: The Monolith
- **LegacyApp**: A single ASP.NET Core application handling Products, Orders, and Customers
- All functionality in one codebase with a single database
- Runs on port 5001

### Step 2: Strangler Facade (Partial Migration)
- **Products.Service**: Extracted microservice handling only Products (port 5002)
- **LegacyApp**: Modified monolith handling Orders and Customers (port 5001)
- **StranglerFacade**: YARP reverse proxy routing requests (port 5000)
  - Routes `/products/**` → Products.Service
  - Routes everything else → LegacyApp
- This step demonstrates the transition phase where both old and new coexist

### Step 3: Full Extraction
- **Products.Service**: Products microservice (port 5002)
- **Orders.Service**: Orders microservice (port 5003)
- **Customers.Service**: Customers microservice (port 5004)
- Each service has its own database and runs independently
- The monolith is fully replaced

## Running the Steps

### Step 1
```bash
cd src/Step1_Monolith/LegacyApp
dotnet run
# Access at http://localhost:5001
```

### Step 2
```bash
# Terminal 1 - Legacy App
cd src/Step2_StranglerFacade/LegacyApp
dotnet run

# Terminal 2 - Products Service
cd src/Step2_StranglerFacade/Products.Service
dotnet run

# Terminal 3 - Strangler Facade
cd src/Step2_StranglerFacade/StranglerFacade
dotnet run
# Access through facade at http://localhost:5000
```

### Step 3
```bash
# Terminal 1
cd src/Step3_FullExtraction/Products.Service
dotnet run

# Terminal 2
cd src/Step3_FullExtraction/Orders.Service
dotnet run

# Terminal 3
cd src/Step3_FullExtraction/Customers.Service
dotnet run
```

## Key Technologies

- **.NET 9**: Latest .NET runtime
- **ASP.NET Core Minimal API**: Lightweight HTTP APIs
- **EF Core 9**: Entity Framework Core for data access
- **SQLite**: Embedded database
- **YARP**: Yet Another Reverse Proxy for request routing

## Benefits of Strangler Fig Pattern

1. **Incremental Migration**: No "big bang" rewrite
2. **Risk Mitigation**: Each service can be tested independently
3. **Business Continuity**: System remains operational throughout migration
4. **Flexibility**: Can pause or adjust strategy at any point
5. **Learning Opportunity**: Team learns microservices incrementally

## Use Cases

- Modernizing legacy applications
- Breaking up monoliths into microservices
- Introducing new technology stack gradually
- Testing microservices architecture before full commitment

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
