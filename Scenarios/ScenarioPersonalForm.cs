using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using JFjewelery.Utility;
using JFjewelery.Scenarios.Interfaces;
using JFjewelery.Models;
using JFjewelery.Services.Interfaces;
using JFjewelery.Extensions;

namespace JFjewelery.Scenarios
{
    public class ScenarioPersonalForm : IBotScenario
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IChatSessionService _sessionService;


        private readonly Dictionary<string, Func<Update, ChatSession, Task>> _steps;

        public ScenarioPersonalForm(ITelegramBotClient botClient, IChatSessionService sessionService)
        {
            _botClient = botClient;
            _sessionService = sessionService;


            //TO DO
            _steps = new()
            {
                ["Question1"] = StepQuestion1Async,
                //["Material"] = StepMaterialAsync,
                //["Size"] = StepSizeAsync,
                //["Confirm"] = StepConfirmAsync
            };
        }

        public string Name => "Personal form";

        //Dictionaries for questions

        //TO DO

        public async Task ExecuteAsync(Update update, CancellationToken cancellationToken)
        {
            var chatId = update.GetChatId();
            var session = await _sessionService.GetOrCteateSessionAsync(chatId)
            ?? throw new Exception("Chat session not found");
            var step = session.ScenarioStep ?? "Question1";

            if (!_steps.TryGetValue(step, out var handler))
            {
                await _botClient.SendTextMessageAsync(chatId, "Unknown step, restar the quiz");
                session.ScenarioStep = "Question1";
                handler = StepQuestion1Async;
            }

            await handler(update, session);
            session.LastUpdated = DateTime.UtcNow;
            await _sessionService.UpdateSessionAsync(session);
        }



        private async Task StepQuestion1Async(Update update, ChatSession session)
        {
            var chatId = update.GetChatId();

            await _botClient.SendTextMessageAsync(chatId, "Now I'm going to ask you a couple of magic questions - " +
                "they will help you find the jewelry that suits you!");
            await _botClient.SendTextMessageAsync(update.GetChatId(), "Какой материал украшения?");
            session.ScenarioStep = "Material";
        }

        //private async Task StepMaterialAsync(Update update, ChatSession session)
        //{
        //    session.TempData["Material"] = update.Message?.Text;
        //    await _bot.SendTextMessageAsync(update.GetChatId(), "Укажите размер:");
        //    session.ScenarioStep = "Size";
        //}

        //private async Task StepSizeAsync(Update update, ChatSession session)
        //{
        //    session.TempData["Size"] = update.Message?.Text;
        //    await _bot.SendTextMessageAsync(update.GetChatId(), "Подтвердите заказ: да/нет");
        //    session.ScenarioStep = "Confirm";
        //}

        //private async Task StepConfirmAsync(Update update, ChatSession session)
        //{
        //    var reply = update.Message?.Text?.ToLower();
        //    if (reply == "да")
        //    {
        //        var material = session.TempData["Material"];
        //        var size = session.TempData["Size"];
        //        await _bot.SendTextMessageAsync(update.GetChatId(), $"Заказ оформлен! Материал: {material}, размер: {size}");
        //    }
        //    else
        //    {
        //        await _bot.SendTextMessageAsync(update.GetChatId(), "Заказ отменён.");
        //    }

        //    session.ScenarioStep = "Start";
        //    session.TempData.Clear();
        //}


    }
}
