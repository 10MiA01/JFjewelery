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
using JFjewelery.Data;
using JFjewelery.Models.Scenario;
using Microsoft.EntityFrameworkCore;

namespace JFjewelery.Scenarios
{
    public class ScenarioPersonalForm : IBotScenario
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IChatSessionService _sessionService;
        private readonly AppDbContext _dbContext;


        public string _scenario;
        private readonly List<Step> _steps;
        private readonly List<Option> _options;

        public List<string> Names => new() { "Personal form", "Custom characteristics", "Custom for an event " };



        public ScenarioPersonalForm(ITelegramBotClient botClient, IChatSessionService sessionService, AppDbContext dbContext)
        {
            _botClient = botClient;
            _sessionService = sessionService;
            _dbContext = dbContext;
            
            _steps = dbContext.Steps
                .Where(s => s.ScenarioId == 1)
                .OrderBy(s => s.Id)
                .ToList();
            _options = dbContext.Options
                .Where(o => _steps.Select(s => s.Id).Contains(o.StepId))
                .ToList();
        }

        


        //TO DO

        public async Task ExecuteAsync(Update update, CancellationToken cancellationToken)
        {
            var chatId = update.GetChatId();
            var session = await _sessionService.GetOrCteateSessionAsync(chatId)
                ?? throw new Exception("Chat session not found");

            _scenario = session.CurrentScenario;
            
            if(_scenario == null)
            {
                _scenario = update.CallbackQuery.Data;
            }

            var step = _steps.FirstOrDefault(s => s.Name == session.ScenarioStep)
                ?? _steps.OrderBy(s => s.Id).First();

            
        }



        private async Task StepQuestion1Async(Update update, ChatSession session)
        {
            var chatId = update.GetChatId();

            await _botClient.SendTextMessageAsync(chatId, "Now I'm going to ask you a couple of magic questions - " +
                "they will help you find the jewelry that suits you!");
            await _botClient.SendTextMessageAsync(update.GetChatId(), "Какой материал украшения?");
            session.ScenarioStep = "Material";
        }



    }
}
