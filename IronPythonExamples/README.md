# IronPythonExamples - Running Python from .NET with IronPython Integration

[![YouTube](https://img.shields.io/badge/YouTube-Dev%20Leader-red?logo=youtube)](https://www.youtube.com/@devleader)
[![Blog](https://img.shields.io/badge/Blog-devleader.ca-blue)](https://www.devleader.ca)
[![Newsletter](https://img.shields.io/badge/Newsletter-subscribe-orange)](https://weekly.devleader.ca)
[![All Links](https://img.shields.io/badge/All%20Links-links.devleader.ca-green)](https://links.devleader.ca)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Nick%20Cosentino-blue?logo=linkedin)](https://www.linkedin.com/in/nickcosentino/)

This .NET Framework 4.0 solution demonstrates five different approaches to integrating Python with C# applications using IronPython 2.7.4. The projects range from simple console output to a full Windows Forms GUI: `PrintToConsole` executes inline Python print statements, `RunExternalScript` loads and runs external `.py` files, `DynamicScript` creates Python classes from inline scripts and calls their methods from C#, `DynamicClass` shows runtime property manipulation on Python objects, and `SampleForm` provides a WinForms application for interactive Python script execution with output redirection to a TextBox.

Each project demonstrates key IronPython integration patterns including engine creation with `Python.CreateEngine()`, script execution with `Execute()` and `ExecuteFile()`, scope management for variable isolation, dynamic object binding using C#'s `dynamic` keyword, and I/O stream redirection for capturing Python output. This is a comprehensive reference for developers looking to embed Python scripting capabilities in their .NET applications.

## Getting Started

### Prerequisites
- .NET Framework 4.0 or later
- IronPython 2.7.4 (installed via NuGet)

### Running the Project
Open the solution in Visual Studio and run any of the five example projects.

## Related Resources

### Blog Articles
- [Running Python from .NET - A Comprehensive Guide for Developers](https://www.devleader.ca/2023/10/22/running-python-from-dotnet-a-comprehensive-guide-for-developers)

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
