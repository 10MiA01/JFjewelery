using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions;



namespace JFjelelery
{
    class Program
    {
        private static ITelegramBotClient bot;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Bot started working");

            //Configuration settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //Bot token
            string botToken = config["TelegramBot:Token"];
            Console.WriteLine($"Bot token: {botToken}");

            //DB connection
            string connectionString = config.GetConnectionString("DefaultConnection");

            //DI-container
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);




            //Init bot
            bot = new TelegramBotClient(botToken);

            var me = await bot.GetMeAsync();
            Console.WriteLine($"Bot @{me.Username} started");

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // получать все обновления
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cts.Token);

            Console.WriteLine("Press Enter to stop");
            Console.ReadLine();

            cts.Cancel();
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                var chatId = update.Message.Chat.Id;
                var messageText = update.Message.Text;

                Console.WriteLine($"Received message from {chatId}: {messageText}");

                // Просто отправляем эхо обратно
                await botClient.SendTextMessageAsync(chatId, $"You said: {messageText}", cancellationToken: cancellationToken);
            }
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }

    }
}
