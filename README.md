# OSSTest

A repository for testing OSS (Open Source Software) detection tools with a .NET 9 Web API.

## Project Structure

- **OSSTestAPI** - A .NET 9 Web API that uses multiple popular open-source packages

## Open Source Packages Included

1. **FusionCache** (v2.5.0) - High-performance caching library
2. **ShapeCrawler** (v0.78.2) - PowerPoint presentation manipulation library
3. **FluentEmail.Core** & **FluentEmail.Smtp** (v3.0.2) - Email templating and sending library

## Quick Start

```bash
cd OSSTestAPI
dotnet restore
dotnet build
dotnet run
```

See [OSSTestAPI/README.md](OSSTestAPI/README.md) for more details.

## Purpose

This repository is designed to test OSS detection tools by incorporating multiple popular open-source NuGet packages in a working .NET 9 Web API application. The implementation is intentionally simple to focus on package detection rather than complex functionality.
