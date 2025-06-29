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
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

using JFjewelery.Utility;
using JFjewelery.Scenarios.Interfaces;
using JFjewelery.Models;
using JFjewelery.Services.Interfaces;
using JFjewelery.Extensions;
using JFjewelery.Data;
using JFjewelery.Models.Scenario;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Enums;

using static JFjewelery.Services.ChatSessionService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;

namespace JFjewelery.Scenarios
{
    public class ScenarioPersonalForm : IBotScenario
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IChatSessionService _sessionService;
        private readonly IButtonComposer _buttonComposer;
        private readonly ICharacteristicsFilter _characteristicsFilter;
        private readonly AppDbContext _dbContext;


        public Scenario _scenario;
        public List<Step> _steps;
        public List<Option> _options;

        public List<string> Names => new() { "Personal form", "Custom characteristics", "Custom for an event " };

        public ScenarioPersonalForm(ITelegramBotClient botClient, IChatSessionService sessionService,IButtonComposer buttonComposer, ICharacteristicsFilter characteristicsFilter, AppDbContext dbContext)
        {
            _botClient = botClient;
            _sessionService = sessionService;
            _buttonComposer = buttonComposer;
            _characteristicsFilter = characteristicsFilter;
            _dbContext = dbContext;
   
        }

        


        //TO DO

        public async Task ExecuteAsync(Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            var chatId = update.GetChatId();
            var session = await _sessionService.GetOrCteateSessionAsync(chatId)
                ?? throw new Exception("Chat session not found");
            var scenario = session.CurrentScenario;
            _scenario = _dbContext.Scenarios
            .Include(s => s.Steps)
                .ThenInclude(step => step.Options)
            .Include(s => s.Steps)
                .ThenInclude(step => step.NextStep)
            .FirstOrDefault(s => s.Name == scenario);


            _steps = _scenario.Steps.OrderBy(s => s.Id).ToList();

            //Choosing a step
            //If first step
            if (session.ScenarioStep == null)
            {
                var firstStep = _steps.First();
                session.ScenarioStep = firstStep.Name;
                await _dbContext.SaveChangesAsync();

                //Send a introductory message
                await _botClient.SendTextMessageAsync(chatId, "Now I'm going to ask you a couple of magic questions - " +
                "they will help you find the jewelry that suits you!");

                //Compose buttons 
                var keyboard =_buttonComposer.CreateKeyboard(firstStep);

                //Send the message to the client
                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{firstStep.QuestionText}",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);

            }

            //If next step
            else if (session.ScenarioStep != null)
            { 
                //Get a step
                var currentStep = _steps.Where(s => s.Name == session.ScenarioStep).FirstOrDefault();
                var optionSelected = update.CallbackQuery.Data;
                var currentOption = currentStep.Options.Where(o => o.Name == optionSelected).FirstOrDefault();


                //get and apply filters
                ProductFilterCriteria filterFromClient = JsonSerializer.Deserialize<ProductFilterCriteria>(currentOption.FilterJson) ?? new ProductFilterCriteria();

                await _sessionService.UpdateFilterCriteriaAsync(chatId, filterFromClient, FilterOperation.Add);

                //Move to next step
                if (currentStep.NextStep == null)
                {
                    await FinishForm(update, cancellationToken);
                    return;
                }
                else
                {
                    currentStep = currentStep.NextStep;
                }

                //Compose buttons 
                var keyboard = _buttonComposer.CreateKeyboard(currentStep);

                //Send the message to the client
                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{currentStep.QuestionText}",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);


            }
        }

        public async Task FinishForm(Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            var chatId = update.GetChatId();
            //Get the filter
            var finishFilter = await _sessionService.GetFilterCriteriaAsync(chatId);
            //Filter in db
            var top3Products = _characteristicsFilter.FilterMatchProductsAsync(finishFilter);
            //send the products 
            await _botClient.SendTextMessageAsync(
               chatId: chatId,
               text: $"Here are product that's perfect match for you!",
               replyMarkup: ,
               cancellationToken: cancellationToken);
            //Reset scenario
            await _sessionService.ResetSessionAsync(chatId);
        }




    }
}
