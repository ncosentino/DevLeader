# ReflectionExamples - .NET Reflection Examples and Benchmarks

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This solution is a comprehensive collection of C# reflection examples covering discovery, attribute scanning, non-public member access, and performance benchmarking. It provides five separate projects that progressively explore the capabilities and trade-offs of reflection in .NET.

**CheckMembers** lists all constructors, properties, and methods of a class with parameter details. **ExampleAppAttributes** demonstrates custom attribute scanning across assemblies using `[MyClassAttribute]` and `[MyMethodAttribute]`. **ExampleAppGetByName** provides an interactive CLI for searching types by substring and inspecting their public and non-public members. **NonPublic** shows how to access private constructors, invoke private methods, and set private field values using reflection, demonstrating reflection's ability to bypass access modifiers. **ActivatorVsInvokeMemberBenchmarks** uses BenchmarkDotNet to compare five different reflection-based instantiation techniques — direct constructor calls, `Activator.CreateInstance()`, `Type.InvokeMember()`, and `ConstructorInfo.Invoke()` — across parameterless, classic, and primary constructor classes.

These examples are paired with Dev Leader blog content and serve as practical references for understanding when and how to use reflection effectively in C# applications.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- An IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

### Running the Project
```bash
# Run the member discovery example
dotnet run --project ReflectionExamples.CheckMembers

# Run the attribute scanning example
dotnet run --project ReflectionExamples.ExampleAppAttributes

# Run the interactive type search
dotnet run --project ReflectionExamples.ExampleAppGetByName

# Run the non-public member access example
dotnet run --project ReflectionExamples.NonPublic

# Run the performance benchmarks (use Release mode)
dotnet run -c Release --project ReflectionExamples.ActivatorVsInvokeMemberBenchmarks
```

## Related Resources

### Blog Articles
- [Reflection in C# - How to Use It](https://www.devleader.ca/2024/02/01/reflection-in-c-how-to-use-it)
- [Activator.CreateInstance vs Type.InvokeMember](https://www.devleader.ca/2024/02/02/activator-createinstance-vs-type-invokemember)

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
