# Repository Analysis Bot

An AI-powered batch tool that analyzes a .NET repository and generates a comprehensive
Markdown report. Point it at any repository and it will explore the codebase autonomously,
then produce an architecture overview, pattern analysis, and improvement recommendations.

## Features

- **Autonomous exploration** -- uses tools to explore structure, read files, find patterns
- **Comprehensive report** -- project overview, architecture, dependencies, patterns, recommendations
- **Zero configuration** -- runs against any directory out of the box
- **Markdown output** -- saves report as `repo-analysis.md` in the repository root

## Setup

Create `appsettings.Development.json`:

```json
{
  "Analyzer": {
    "GithubToken": "ghp_your_token_here"
  }
}
```

## Running

```bash
# Analyze current directory
dotnet run

# Analyze a specific repository
dotnet run -- C:\path\to\your\repo

# Custom output location
# Set "OutputPath" in appsettings.json
```

## Output

The bot generates `repo-analysis.md` with these sections:

- **Project Overview** -- What the repository does and its purpose
- **Architecture & Structure** -- How the codebase is organized
- **Technologies & Dependencies** -- Frameworks and packages in use
- **Code Patterns & Practices** -- Design patterns and conventions observed
- **Observations & Recommendations** -- Actionable improvement suggestions
