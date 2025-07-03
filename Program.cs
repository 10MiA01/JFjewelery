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

        //Base url
        var baseUrl = builder.Configuration["BaseUrl"];
        var baseUri = new Uri(baseUrl!);
        var host = baseUri.Host;         // "192.168.0.115"
        var port = baseUri.Port;         // 50413

        builder.Services.AddSingleton(baseUri);

        //Clean URL's
        builder.WebHost.UseUrls();
        // allow to listen not only the localhost, but also external IP-connections
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(System.Net.IPAddress.Any, port);
        });


        // Telegram bot
        var botToken = configuration["TelegramBot:Token"];
        builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

        ////Proxy
        //builder.Services.AddSingleton<ITelegramBotClient>(sp =>
        //{
        //    var proxy = new HttpToSocks5Proxy(host, port);
        //    var handler = new HttpClientHandler
        //    {
        //        Proxy = proxy,
        //        UseProxy = true
        //    };

        //    var httpClient = new HttpClient(handler);

        //    return new TelegramBotClient(botToken, httpClient);
        //});



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

