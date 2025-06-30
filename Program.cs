using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;


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
        //Configuration by host
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                
                //Bot in DI
                var botToken = configuration["TelegramBot:Token"];
                services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

                //DB context
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

                    })
                    .EnableSensitiveDataLogging()
                    );

                //Old config
                //services.AddDbContext<AppDbContext>(options =>
                //options.UseNpgsql(connectionString));

                //Services_SCOPED
                services.AddScoped<ICustomerService, CustomerService>();
                services.AddScoped<IChatSessionService, ChatSessionService>();
                services.AddScoped<IButtonComposer, ButtonComposer>();
                services.AddScoped<ICharacteristicsFilter, CharacteristicsFilter>();

                //Bot-scenarios
                services.AddScoped<IBotScenario, ScenarioPersonalForm>();


                //Add bot to host
                services.AddHostedService<BotService>();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    // Static files from Media
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Media")),
                        RequestPath = "/Media"
                    });
                });

                // Servder accessed by url
                webBuilder.UseUrls("http://localhost:5000");
            })
            .Build();

        await host.RunAsync();
    }
}
