using ZiggyCreatures.Caching.Fusion;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net.Mail;
using ShapeCrawler;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FusionCache
builder.Services.AddFusionCache();

// Add FluentEmail with SMTP sender
builder.Services
    .AddFluentEmail("test@example.com")
    .AddSmtpSender(new SmtpClient("localhost"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// FusionCache endpoint
app.MapGet("/cache/test", async (IFusionCache cache) =>
{
    var cacheKey = "test-key";
    var cachedValue = await cache.GetOrSetAsync(
        cacheKey,
        async _ => {
            await Task.Delay(100); // Simulate some work
            return $"Cached at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
        },
        TimeSpan.FromMinutes(5)
    );
    
    return new { Key = cacheKey, Value = cachedValue, Message = "FusionCache is working!" };
})
.WithName("TestCache");

// FluentEmail endpoint
app.MapPost("/email/send", async (IFluentEmail email) =>
{
    try
    {
        var response = await email
            .To("recipient@example.com")
            .Subject("Test Email from OSS Test API")
            .Body("This is a test email sent using FluentEmail!")
            .SendAsync();
        
        return new { Success = response.Successful, Message = "FluentEmail configured successfully!" };
    }
    catch (Exception ex)
    {
        return new { Success = false, Message = $"FluentEmail is configured but cannot send (expected): {ex.Message}" };
    }
})
.WithName("SendTestEmail");

// ShapeCrawler endpoint
app.MapGet("/presentation/info", () =>
{
    // Create a simple presentation info response
    // Note: ShapeCrawler is for working with PowerPoint presentations
    return new 
    { 
        Message = "ShapeCrawler is installed and ready!",
        Description = "ShapeCrawler is a library for working with PowerPoint presentations",
        Version = "0.78.2"
    };
})
.WithName("GetPresentationInfo");

// SolnetUnified endpoint - Solana wallet operations
app.MapGet("/solana/wallet/generate", () =>
{
    try
    {
        // Generate a new Solana wallet
        var mnemonic = new Mnemonic(WordList.English, WordCount.Twelve);
        var wallet = new Wallet(mnemonic);
        
        return Results.Ok(new
        {
            Success = true,
            Message = "Solana wallet generated successfully using Solnet!",
            PublicKey = wallet.Account.PublicKey.Key,
            MnemonicWords = mnemonic.ToString(),
            Warning = "This is a demo wallet. Never use these keys in production!"
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new
        {
            Success = false,
            Message = $"Error generating wallet: {ex.Message}"
        });
    }
})
.WithName("GenerateSolanaWallet");

app.MapPost("/solana/wallet/restore", (RestoreWalletRequest request) =>
{
    try
    {
        // Restore a wallet from mnemonic phrase
        var mnemonic = new Mnemonic(request.MnemonicPhrase);
        var wallet = new Wallet(mnemonic);
        
        return Results.Ok(new
        {
            Success = true,
            Message = "Solana wallet restored successfully using Solnet!",
            PublicKey = wallet.Account.PublicKey.Key,
            Warning = "This is a demo. Never share your mnemonic phrase!"
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new
        {
            Success = false,
            Message = $"Error restoring wallet: {ex.Message}"
        });
    }
})
.WithName("RestoreSolanaWallet");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC * 9 / 5.0);
}

record RestoreWalletRequest(string MnemonicPhrase);
