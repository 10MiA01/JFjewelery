using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using MihaZupan;
using System.Net.Http;


using JFjewelery.Utility;
using JFjewelery.Data;
using JFjewelery.Services;
using JFjewelery.Services.Interfaces;
using JFjewelery.Scenarios.Interfaces;
using JFjewelery.Scenarios;




namespace JFjelelery;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        // Determine the current environment
        var isProd = builder.Environment.IsProduction();

        // Get baseUrl depending on the environment
        var listenUrl = configuration["MyApp:ListenAddress"];
        var staticBaseUrl = configuration["MyApp:StaticBaseUrl"];

        var listenUri = new Uri(listenUrl!);
        var staticUri = new Uri(staticBaseUrl!);

        builder.Services.AddSingleton(staticUri);

        Console.WriteLine($"[DEBUG] Base URL: {staticUri}");

        // If not in production, extract host and port and configure Kestrel manually
        var apiBaseUrl = builder.Configuration["ApiManager:BaseUrl"];
        if (string.IsNullOrWhiteSpace(apiBaseUrl))
        {
            throw new InvalidOperationException("ApiManager:BaseUrl is not set in configuration.");
        }
        Console.WriteLine($"[DEBUG] API Base URL: {apiBaseUrl}");


        // Get Url for api
        builder.Services.AddHttpClient<IApiManager, ApiManager>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        Console.WriteLine($"[DEBUG] Base URL: {staticUri}");

        
        // Telegram bot
        var botToken = configuration["TelegramBot:Token"];
        builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

        // DB context
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .EnableSensitiveDataLogging()
        );

        // Services
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IChatSessionService, ChatSessionService>();
        builder.Services.AddScoped<IButtonComposer, ButtonComposer>();
        builder.Services.AddScoped<ICharacteristicsFilter, CharacteristicsFilter>();
        builder.Services.AddScoped<IFileManager, FileManager>();

        //Scenarios
        builder.Services.AddScoped<IBotScenario, ScenarioPersonalForm>();
        builder.Services.AddScoped<IBotScenario, ScenarioImageCustom>();

        // Bot background service
        builder.Services.AddHostedService<BotService>();

        var app = builder.Build();

        // Connect static files from the folder Media
        var provider = new FileExtensionContentTypeProvider();
        provider.Mappings[".jpg"] = "image/jpeg";
        provider.Mappings[".jpeg"] = "image/jpeg";
        provider.Mappings[".png"] = "image/png";


        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Media")),
            RequestPath = "/Media",
            ContentTypeProvider = provider,
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                ctx.Context.Response.Headers["Pragma"] = "no-cache";
                ctx.Context.Response.Headers["Expires"] = "0";
            }
        });

        await app.RunAsync();
    }
}

