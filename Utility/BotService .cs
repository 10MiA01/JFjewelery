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
using JFjewelery.Extensions;
using JFjewelery.Data;
using Microsoft.EntityFrameworkCore;
using JFjewelery.Models.Scenario;
using JFjewelery.Models;



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

            var _scenarioServices = scope.ServiceProvider.GetServices<IBotScenario>();
            var _customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var _chatSessionService = scope.ServiceProvider.GetRequiredService<IChatSessionService>();
            var _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var _scenarios = await _dbContext.Scenarios.ToListAsync();
            var _currentSession = await _chatSessionService.GetOrCteateSessionAsync(update);

            if (update.Type == UpdateType.CallbackQuery)
            {
                Console.WriteLine("CallbackQuery received:");
                Console.WriteLine(update.CallbackQuery.Data);
            }

            //For random message
            if (_currentSession?.CurrentScenario == null  && update.Type == UpdateType.Message && update.Message!.Text != null && update.Message?.Text != "/start")
            {
                var telegramAcc = update.Message.From?.Username
                    ?? update.Message.From?.Id.ToString();
                var chatId = update.GetChatId();
                if (telegramAcc != null)
                {
                    var customer = await _customerService.GetOrCreateCustomerAsync(chatId, telegramAcc);
                    await _chatSessionService.ResetSessionAsync(chatId);

                }

                var messageText = update.Message.Text;

                Console.WriteLine($"Message from {chatId}: {messageText}");

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Send /start to start chat",
                    cancellationToken: cancellationToken);
            }

            //If /start
            if (update.Type == UpdateType.Message && update.Message?.Text == "/start")
            {
                //Cheking if we have the customer, if not yet=>create, reset chat state
                var telegramAcc = update.Message.From?.Username
                    ?? update.Message.From?.Id.ToString();
                var chatId = update.GetChatId();

                if (telegramAcc != null)
                {
                    var customer = await _customerService.GetOrCreateCustomerAsync(chatId, telegramAcc);
                    await _chatSessionService.ResetSessionAsync(chatId);

                }

                //Scenario buttons
                var buttons = _scenarios
                    .Select(s => InlineKeyboardButton.WithCallbackData(s.Name, s.Name))
                    .ToArray();
                var keyboard = new InlineKeyboardMarkup(buttons.Chunk(2));


                //Send the message to the client
                await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Choose an option:",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);
            }

            //Running the scenario
            else if (
                update.Type == UpdateType.CallbackQuery ||      //Standart scenario
                (
                    update.Type == UpdateType.Message &&        //Picture scenario
                    (
                        update.Message?.Photo?.Any() == true ||
                        (update.Message?.Document != null && update.Message.Document.MimeType?.StartsWith("image/") == true)
                    ) &&
                    (_currentSession?.CurrentScenario == "Custom by picture" || _currentSession?.CurrentScenario == "Virtual fitting")
                ) ||
                (_currentSession?.CurrentScenario == "Virtual fitting" && update.Type == UpdateType.Message && update.Message!.Text != null && update.Message?.Text != "/start")
            )
            {
                var telegramAcc = update.CallbackQuery?.From?.Username
                    ?? update.Message?.From?.Username
                    ?? update.CallbackQuery?.From?.Id.ToString()
                    ?? update.Message?.From?.Id.ToString();


                var chatId = update.GetChatId();
                var customer = await _customerService.GetOrCreateCustomerAsync(chatId, telegramAcc);
                var session = customer.ChatSession;
                if ( session == null) { await _chatSessionService.GetOrCteateSessionAsync(update); } 
                    
                    

                if (update.CallbackQuery != null && session!.CurrentScenario == null)
                {
                    var callbackData = update.CallbackQuery.Data;
                    var scenarioFromButton = _scenarios.FirstOrDefault(s => s.Name == callbackData);

                    // Scenario is just selected => set scenario to session
                    if (session!.CurrentScenario == null && scenarioFromButton != null)
                    {
                        session.CurrentScenario = scenarioFromButton.Name;
                        session.ScenarioStep = null;
                        await _chatSessionService.UpdateSessionAsync(session);

                        var handler = _scenarioServices
                            .FirstOrDefault(s => s.Names.Contains(session.CurrentScenario));

                        if (handler != null)
                        {
                            await handler.ExecuteAsync(update, cancellationToken);
                        }
                    }
                }
         

                // Scenario is already selected => continue
                else if ((update.CallbackQuery != null && session!.CurrentScenario != null) ||
                    (update.Type == UpdateType.Message &&        //Picture scenario
                    (
                        update.Message?.Photo?.Any() == true ||
                        (update.Message?.Document != null && update.Message.Document.MimeType?.StartsWith("image/") == true)
                    ) &&
                    (_currentSession?.CurrentScenario == "Custom by picture" || _currentSession?.CurrentScenario == "Virtual fitting")) ||
                    (_currentSession?.CurrentScenario == "Virtual fitting" && update.Type == UpdateType.Message && update.Message!.Text != null && update.Message?.Text != "/start")
                    )
                {
                    Console.WriteLine("Trying to get in scenario 5_2");
                    var handler = _scenarioServices
                        .FirstOrDefault(s => s.Names.Contains(session.CurrentScenario));

                    if (handler != null)
                    {
                        await handler.ExecuteAsync(update, cancellationToken);
                    }
                }

                //Other
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId,
                        "The selected option could not be recognized.",
                        cancellationToken: cancellationToken);
                }
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"❌ Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
