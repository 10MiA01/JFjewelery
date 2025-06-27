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
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.DependencyInjection;

using JFjewelery.Services;
using JFjewelery.Services.Interfaces;
using JFjewelery.Scenarios;
using JFjewelery.Scenarios.Interfaces;



namespace JFjewelery.Utility
{
    public class BotService : BackgroundService
    {
        private readonly ITelegramBotClient _botClient;

        private readonly IServiceScopeFactory _scopeFactory;

        public BotService(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
        {
            _botClient = botClient;
            _scopeFactory = scopeFactory;
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

            using var scope = _scopeFactory.CreateScope();

            var _scenarios = scope.ServiceProvider.GetServices<IBotScenario>();
            var _customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var _chatSessionService = scope.ServiceProvider.GetRequiredService<IChatSessionService>();


            //For random message
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

            //If /start
            if(update.Type == UpdateType.Message && update.Message?.Text == "/start")
            {
                //Cheking if we have the customer, if not yet=>create, reset chat state
                var telegramAcc = update.Message.From?.Username
                    ?? update.Message.From?.Id.ToString();

                if (telegramAcc != null)
                {
                    var customer = await _customerService.GetOrCreateCustomerAsync(telegramAcc);
                    _chatSessionService.ResetSessionAsync(customer.Id);

                }

                //Scenario buttons
                var buttons = _scenarios.Select(s => InlineKeyboardButton.WithCallbackData(s.Name, s.Name)).ToArray();
                var keyboard = new InlineKeyboardMarkup(buttons.Chunk(2));


                //Send the message to the client
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
