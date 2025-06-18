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
using JFjewelery.Services.Interfaces;

namespace JFjewelery.Services
{
    public class ScenarioPersonalForm :IBotScenario
    {
        private readonly ITelegramBotClient _botClient;

        public ScenarioPersonalForm(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public string Name => "Personal form";

        public async Task ExecuteAsync(Update update, CancellationToken ct)
        {
            var chatId = update.CallbackQuery!.Message.Chat.Id;
            await _botClient.SendTextMessageAsync(chatId, "Personal from scenario", cancellationToken: ct);
        }
    }
}
