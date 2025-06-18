using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using JFjewelery.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;


namespace JFjewelery.Utility
{
    public class BotService : BackgroundService
    {
        private readonly ITelegramBotClient _botClient;

        private readonly IEnumerable<IBotScenario> _scenarios;

        public BotService(ITelegramBotClient botClient, IEnumerable<IBotScenario> scenarios)
        {
            _botClient = botClient;
            _scenarios = scenarios;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions
                {
                    AllowedUpdates = Array.Empty<UpdateType>()
                },
                cancellationToken: cts.Token);

            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"Bot @{me.Username} is running...");

            await Task.Delay(-1, stoppingToken); // run forever
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // USE BOTCLIENT FROM PARAMETRS!!!
            
            if (update.Type == UpdateType.Message && update.Message!.Text != null && update.Message?.Text != "/start")
            {
                var chatId = update.Message.Chat.Id;
                var messageText = update.Message.Text;

                Console.WriteLine($"Message from {chatId}: {messageText}");

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Send /start to start chat",
                    cancellationToken: cancellationToken);
            }

            //Scenario buttons
            if(update.Type == UpdateType.Message && update.Message?.Text == "/start")
            {
                var buttons = _scenarios.Select(s => InlineKeyboardButton.WithCallbackData(s.Name, s.Name)).ToArray();
                var keyboard = new InlineKeyboardMarkup(buttons.Chunk(2));


                await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Choose an option:",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);
            }

            //Running the scenario
            else if (update.Type == UpdateType.CallbackQuery)
            {
                var scenarioName = update.CallbackQuery.Data;
                var scenario = _scenarios.FirstOrDefault(s => s.Name == scenarioName);

                if (scenario != null)
                    await scenario.ExecuteAsync(update, cancellationToken);
                else
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Option not found", cancellationToken: cancellationToken);
            }


        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"❌ Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
