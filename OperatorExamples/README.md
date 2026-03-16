# OperatorExamples - Operator Overloading and Implicit Operators in C#

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This project demonstrates implicit operator overloading in C# for seamless type conversions between custom domain types and primitive types. It showcases how implicit operators can make strongly-typed code more readable and intuitive by allowing automatic conversions without explicit casts.

The examples include measurement types (`LengthInM` and `TemperatureInC`) that implicitly convert to `double` for arithmetic operations, a `ComplexNumber` struct that accepts implicit conversions from `int` and `double` with custom arithmetic operator overloading, and a `Money` struct with bidirectional implicit conversions to and from `double`. These practical examples demonstrate how implicit operators simplify working with domain-specific types while maintaining type safety.

Whether you are building financial applications, scientific computing tools, or any domain where custom value types are common, understanding implicit operator overloading is essential for writing clean, expressive C# code.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
dotnet run --project OperatorExamples.ImplicitOperators
```

## Related Resources

### Blog Articles
- [Operator Overloading in C# - How to Do It Right](https://www.devleader.ca/2024/02/24/operator-overloading-in-c-how-to-do-it-right)
- [Implicit Operators in C# - How to Simplify Type Conversions](https://www.devleader.ca/2024/03/04/implicit-operators-in-c-how-to-simplify-type-conversions/)
- [Implicit Operators - Clean Code Secrets Or Buggy Nightmare?](https://www.devleader.ca/2023/08/04/implicit-operators-clean-code-secrets-or-buggy-nightmare)

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
