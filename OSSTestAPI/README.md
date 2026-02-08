# OSS Test API

A .NET 9 Web API project demonstrating the integration of popular open-source packages for OSS detection tool testing.

## Packages Used

1. **FusionCache** (v2.5.0) - A high-performance, easy-to-use cache library with advanced features
2. **ShapeCrawler** (v0.78.2) - A .NET library for working with PowerPoint presentations
3. **FluentEmail.Core** & **FluentEmail.Smtp** (v3.0.2) - Email templating and sending library

## API Endpoints

### 1. Weather Forecast
- **GET** `/weatherforecast`
- Returns sample weather forecast data
- Default endpoint from template

### 2. Cache Test
- **GET** `/cache/test`
- Demonstrates FusionCache functionality
- Caches a timestamp value for 5 minutes

### 3. Email Send
- **POST** `/email/send`
- Demonstrates FluentEmail configuration
- Note: Will not actually send emails without proper SMTP configuration

### 4. Presentation Info
- **GET** `/presentation/info`
- Shows ShapeCrawler package information
- Demonstrates the package is installed and available

## Running the Application

```bash
cd OSSTestAPI
dotnet restore
dotnet build
dotnet run
```

The application will start at `http://localhost:5220` by default.

## Testing with curl

```bash
# Test cache endpoint
curl http://localhost:5220/cache/test

# Test weather forecast
curl http://localhost:5220/weatherforecast

# Test email endpoint
curl -X POST http://localhost:5220/email/send

# Test presentation info
curl http://localhost:5220/presentation/info
```

## Purpose

This project is designed to test OSS detection tools by incorporating multiple popular open-source NuGet packages in a working .NET 9 Web API application.
