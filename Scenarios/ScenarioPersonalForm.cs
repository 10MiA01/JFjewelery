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
        private readonly Uri _staticBaseUri;
        private readonly ITelegramBotClient _botClient;
        private readonly IChatSessionService _sessionService;
        private readonly IButtonComposer _buttonComposer;
        private readonly ICharacteristicsFilter _characteristicsFilter;
        private readonly AppDbContext _dbContext;



        public Scenario _scenario;
        public List<Step> _steps;
        public List<Option> _options;

        public List<string> Names => new() { "Personal form", "Custom characteristics", "Custom for an event" };

        public ScenarioPersonalForm(Uri staticBaseUri, ITelegramBotClient botClient, IChatSessionService sessionService,IButtonComposer buttonComposer, ICharacteristicsFilter characteristicsFilter, AppDbContext dbContext)
        {
            _staticBaseUri = staticBaseUri; //reference, not an instance
            _botClient = botClient;
            _sessionService = sessionService;
            _buttonComposer = buttonComposer;
            _characteristicsFilter = characteristicsFilter;
            _dbContext = dbContext;
   
        }


        public async Task ExecuteAsync(Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            var chatId = update.GetChatId();
            var session = await _sessionService.GetOrCteateSessionAsync(update)
                ?? throw new Exception("Chat session not found");
            var scenario = session.CurrentScenario;
            _scenario = await _dbContext.Scenarios
            .Include(s => s.Steps)
                .ThenInclude(step => step.Options)
            .Include(s => s.Steps)
                .ThenInclude(step => step.NextStep)
            .FirstOrDefaultAsync(s => s.Name == scenario);

            if (_scenario == null)
                throw new Exception("Scenario could not be resolved.ScenarioPersonalForm_3");


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
                var keyboard =_buttonComposer.CreateKeyboard(firstStep, ExtraButtonType.Cancel);

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
                //Quiz is cancelled
                if (update.CallbackQuery.Data == "Cancel")
                {
                    await _sessionService.ResetSessionAsync(chatId);

                    await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Soryy, if you didn't like the quiz, but you can choose another one! Just send /start",
                    cancellationToken: cancellationToken);
                    return;
                }

                //Get a step
                var currentStep = _steps.Where(s => s.Name == session.ScenarioStep).FirstOrDefault();

                //Loging for debug
                Console.WriteLine($"Step: {currentStep.Name}, Options: {string.Join(", ", currentStep.Options.Select(o => o.Name))}");

                //Null check
                if (currentStep == null)
                    throw new Exception("currentStep could not be resolved.ScenarioPersonalForm_1");
                if(update.CallbackQuery.Data == null)
                    throw new Exception("Responce could not be resolved.ScenarioPersonalForm_2");

                var optionSelected = update.CallbackQuery.Data;

                //Loging for debug
                Console.WriteLine($"User selected option: {optionSelected}");
                var currentOption = currentStep.Options.Where(o => o.Name == optionSelected).FirstOrDefault();
                
                //Quiz is finished before the last question
                if (update.CallbackQuery.Data == "Finish")
                {
                    await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Here is the result based on your replies! Hope you'll like :)",
                    cancellationToken: cancellationToken);

                    await FinishForm(update, cancellationToken);

                    await _sessionService.ResetSessionAsync(chatId);
                    return;
                }

                //Null check
                if (currentOption == null)
                {
                    await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"An Error occured. Send /start to start chat",
                    cancellationToken: cancellationToken);
                    //return;
                }

                //get and apply filters
                ProductFilterCriteria filterFromClient;
                if (currentOption.FilterJson == null || string.IsNullOrEmpty(session.FilterJson))
                {
                    filterFromClient = new ProductFilterCriteria();
                }
                else
                {
                    filterFromClient = JsonSerializer.Deserialize<ProductFilterCriteria>(currentOption.FilterJson) ?? new ProductFilterCriteria();
                }

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
                    session.ScenarioStep = currentStep.Name;
                    await _sessionService.UpdateSessionAsync(session);
                }

                //Compose buttons 
                var keyboard = _buttonComposer.CreateKeyboard(currentStep, ExtraButtonType.FinishAndCancel);

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
            var top3Products = await _characteristicsFilter.FilterMatchProductsAsync(finishFilter);
            //send the products 
            await _botClient.SendTextMessageAsync(
               chatId: chatId,
               text: $"Here are product that's perfect match for you!",
               cancellationToken: cancellationToken);

            foreach (var product in top3Products)
            {
                var relativePath = product.Images?.FirstOrDefault()?.FilePath;

                Console.WriteLine($"image url: {relativePath}");

                if (!string.IsNullOrEmpty(relativePath))
                {
                    var imageUrl = new Uri(_staticBaseUri, relativePath.Replace("\\", "/")).ToString();
                    Console.WriteLine($"Full image URL: {imageUrl}");

                    await _botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: InputFile.FromUri(imageUrl), 
                        caption: $"{product.Name}\nPrice: {product.Price}\nQuantity: {product.Quantity}",
                        cancellationToken: cancellationToken);
                }
                else
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{product.Name}\nPrice: {product.Price}\nQuantity: {product.Quantity}",
                        cancellationToken: cancellationToken);
                }
            }

            //Reset scenario
            await _sessionService.ResetSessionAsync(chatId);
        }




    }
}
