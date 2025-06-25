using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using JFjewelery.Utility;
using JFjewelery.Scenarios.Interfaces;
using JFjewelery.Models;
using JFjewelery.Services.Interfaces;

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
                ["Start"] = StepStartAsync,
                ["Material"] = StepMaterialAsync,
                ["Size"] = StepSizeAsync,
                ["Confirm"] = StepConfirmAsync
            };
        }

        public string Name => "Personal form";


        //TO DO

        public async Task ExecuteAsync(Update update, ChatSession session, CancellationToken cancellationToken = default)
        {
            var chatId = update.CallbackQuery!.Message.Chat.Id;
            var step = session.ScenarioStep ?? "Start";
            if (!_steps.TryGetValue(step, out var handler))
            {
                await _bot.SendTextMessageAsync(update.GetChatId(), "Неизвестный шаг. Начинаем заново.");
                session.ScenarioStep = "Start";
                handler = StepStartAsync;
            }

            await handler(update, session);
            session.LastUpdated = DateTime.UtcNow;
            await _sessionService.UpdateAsync(session);
        }


        
        private async Task StepStartAsync(Update update, ChatSession session)
        {
            await _bot.SendTextMessageAsync(update.GetChatId(), "Какой материал украшения?");
            session.ScenarioStep = "Material";
        }

        private async Task StepMaterialAsync(Update update, ChatSession session)
        {
            session.TempData["Material"] = update.Message?.Text;
            await _bot.SendTextMessageAsync(update.GetChatId(), "Укажите размер:");
            session.ScenarioStep = "Size";
        }

        private async Task StepSizeAsync(Update update, ChatSession session)
        {
            session.TempData["Size"] = update.Message?.Text;
            await _bot.SendTextMessageAsync(update.GetChatId(), "Подтвердите заказ: да/нет");
            session.ScenarioStep = "Confirm";
        }

        private async Task StepConfirmAsync(Update update, ChatSession session)
        {
            var reply = update.Message?.Text?.ToLower();
            if (reply == "да")
            {
                var material = session.TempData["Material"];
                var size = session.TempData["Size"];
                await _bot.SendTextMessageAsync(update.GetChatId(), $"Заказ оформлен! Материал: {material}, размер: {size}");
            }
            else
            {
                await _bot.SendTextMessageAsync(update.GetChatId(), "Заказ отменён.");
            }

            session.ScenarioStep = "Start";
            session.TempData.Clear();
        }
    }
}
