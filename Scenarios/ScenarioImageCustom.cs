using JFjewelery.Data;
using JFjewelery.Extensions;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Enums;
using JFjewelery.Models.Scenario;
using JFjewelery.Scenarios.Interfaces;
using JFjewelery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace JFjewelery.Scenarios
{
    public class ScenarioImageCustom : IBotScenario
    {
        

        private readonly Uri _staticBaseUri;
        private readonly ITelegramBotClient _botClient;
        private readonly IChatSessionService _sessionService;
        private readonly IButtonComposer _buttonComposer;
        private readonly ICharacteristicsFilter _characteristicsFilter;
        private readonly AppDbContext _dbContext;
        private readonly IFileManager _fileManager;
        private readonly IApiManager _apiManager;

        public List<string> Names => new() { "Custom by picture" };

        public Scenario _scenario;
        public List<Step> _steps;

        public ScenarioImageCustom(Uri staticBaseUri, ITelegramBotClient botClient, IChatSessionService sessionService, IButtonComposer buttonComposer, 
            ICharacteristicsFilter characteristicsFilter, AppDbContext dbContext, IFileManager fileManager, IApiManager apiManager)
        {
            _staticBaseUri = staticBaseUri; //reference, not an instance
            _botClient = botClient;
            _sessionService = sessionService;
            _buttonComposer = buttonComposer;
            _characteristicsFilter = characteristicsFilter;
            _dbContext = dbContext;
            _fileManager = fileManager;
            _apiManager = apiManager;
        }


        public async Task ExecuteAsync(Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {

            //Get chat info
            var chatId = update.GetChatId();
            var session = await _sessionService.GetOrCteateSessionAsync(update)
                ?? throw new Exception("Chat session not found");

            //Scenario info
            var scenario = session.CurrentScenario;
            _scenario = await _dbContext.Scenarios
            .Include(s => s.Steps)
                .ThenInclude(step => step.NextStep)
            .FirstOrDefaultAsync(s => s.Name == scenario);

            if (_scenario == null)
                throw new Exception("Scenario could not be resolved.ScenarioPersonalForm_3");

            _steps = _scenario.Steps.OrderBy(s => s.Id).ToList();

            if (session.ScenarioStep == null)
            {
                //Set a step
                var firstStep = _steps.First();
                session.ScenarioStep = firstStep.Name;
                await _dbContext.SaveChangesAsync();

                var keyboard = _buttonComposer.CreateKeyboard(firstStep, ExtraButtonType.Cancel);

                //Send a message to get a picture
                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Please send a picture as a reference for your jewels!",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);

                return;
            }

            //Quiz is cancelled
            if (update.CallbackQuery?.Data == "Cancel")
            {
                await _sessionService.ResetSessionAsync(chatId);

                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Probaly you wanted to choose another one? ;) Just send /start",
                cancellationToken: cancellationToken);
                return;
            }


            //Get the picture
            var bytePicture = await _fileManager.DownloadPhotoAsync(update, cancellationToken);
            if (bytePicture == null)
            {
                throw new Exception("image could not be resolved");
            }

            //Give it to Api and have product filter back
            var responceFilter = await _apiManager.AnalyzeImageAsync(bytePicture);

            //Implement filter and return result
            await _sessionService.UpdateFilterCriteriaAsync(chatId, responceFilter, FilterOperation.Add);

            //Finish form
            await FinishForm(update, cancellationToken);
            return;
            
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
               text: $"Here are product that's perfect match for your picture!",
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
