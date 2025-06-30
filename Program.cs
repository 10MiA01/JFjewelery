using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;


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
        builder.Services.AddScoped<IBotScenario, ScenarioPersonalForm>();

        // Bot background service
        builder.Services.AddHostedService<BotService>();

        var app = builder.Build();

        // 👉 Подключаем статические файлы из папки Media
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Media")),
            RequestPath = "/Media"
        });

        await app.RunAsync();
    }
}

