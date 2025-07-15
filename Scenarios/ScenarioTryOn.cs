using JFjewelery.Data;
using JFjewelery.Extensions;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Enums;
using JFjewelery.Models.Helpers;
using JFjewelery.Models.Scenario;
using JFjewelery.Scenarios.Interfaces;
using JFjewelery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace JFjewelery.Scenarios
{
    public class ScenarioTryOn : IBotScenario
    {

        private readonly Uri _staticBaseUri;
        private readonly ITelegramBotClient _botClient;
        private readonly IChatSessionService _sessionService;
        private readonly IButtonComposer _buttonComposer;
        private readonly ICharacteristicsFilter _characteristicsFilter;
        private readonly AppDbContext _dbContext;
        private readonly IFileManager _fileManager;
        private readonly IApiManager _apiManager;

        public List<string> Names => new() { "Virtual fitting" };

        public Scenario _scenario;
        public List<Step> _steps;

        public ScenarioTryOn(Uri staticBaseUri, ITelegramBotClient botClient, IChatSessionService sessionService, IButtonComposer buttonComposer,
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
                throw new Exception("Scenario could not be resolved.ScenarioTryOn_1");

            _steps = _scenario.Steps.OrderBy(s => s.Id).ToList();

            var categories = await _dbContext.Products
                .Select(p => p.Category.Name)
                .Distinct()
                .ToListAsync();

            if (session.ScenarioStep == null)
            {
                //Set a step
                var firstStep = _steps.First();
                session.ScenarioStep = firstStep.Name;
                await _dbContext.SaveChangesAsync();

                var keyboard = _buttonComposer.CreateFromCategories(categories, ExtraButtonType.Cancel);

                //Send a message to get a picture
                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Please a category of product you want to try on.",
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);

                return;
            }

            else if (session.ScenarioStep != null && update.CallbackQuery != null)
            {

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

                //Set next step
                var currentStep = _steps.Where(s => s.Name == session.ScenarioStep).FirstOrDefault();


                //Analyze category selected => chat session Temp data
                var categorySelected = update.CallbackQuery.Data.Replace(" ", "_");

                //Get the instruvction
                if (Enum.TryParse<CategoryTryOnInstructions>(categorySelected, out var categoryEnum))
                {
                    string instruction = CategoryInstructions.Instructions[categoryEnum];

                    // Send instruction for photo
                    await _botClient.SendTextMessageAsync(chatId, instruction);
                }
                else
                {
                    // Instruction not found
                    Console.WriteLine("Instruction not found");
                }

                //Null check
                if (currentStep == null)
                    throw new Exception("currentStep could not be resolved.ScenarioPersonalForm_1");
                if (update.CallbackQuery.Data == null)
                    throw new Exception("Responce could not be resolved.ScenarioPersonalForm_2");

                session.TempData = categorySelected;

                //Move to next step
                currentStep = currentStep.NextStep;
                session.ScenarioStep = currentStep.Name;
                await _sessionService.UpdateSessionAsync(session);
                
            }

            else if (session.ScenarioStep != null && update.Message?.Photo?.Any() == true ||
                    (update.Message?.Document != null && update.Message.Document.MimeType?.StartsWith("image/") == true))
            {
                //Get the picture
                var bytePicture = await _fileManager.DownloadPhotoAsync(update, cancellationToken);
                if (bytePicture == null)
                {
                    throw new Exception("image could not be resolved");
                }

                //Get category and product
                var categorySelected = session.TempData;
                var productsForCategory = await _dbContext.Products
                    .Include(p => p.Category)
                    .Where(p => p.Category.Name == categorySelected)
                    .ToListAsync();

                // Null check
                if (productsForCategory.Count == 0)
                {
                    throw new Exception("No products in such category");
                }

                // Get random product
                var random = new Random();
                var randomProduct = productsForCategory[random.Next(productsForCategory.Count)];




                //Give picture and a product image? it to Api and have product filter back
                //To REDO
                var image = await _apiManager.AnalyzeImageAsync(bytePicture);


                //Save picture in temp data
                //Implement filter and return result

                //Finish form
                await FinishForm(update, cancellationToken);
                return;
            }
        }

        public async Task FinishForm(Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            var chatId = update.GetChatId();
            var session = await _sessionService.GetOrCteateSessionAsync(update)
                ?? throw new Exception("Chat session not found");
            //Get the temp data
            var finalPictureBytes = session.TempData;
            //Send a restored picture

            //Reset scenario
            await _sessionService.ResetSessionAsync(chatId);
        }

    }
}
