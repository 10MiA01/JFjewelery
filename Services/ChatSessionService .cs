using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.EntityFrameworkCore;

using JFjewelery.Data;
using JFjewelery.Models;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Enums;
using JFjewelery.Services.Interfaces;
using JFjewelery.Extensions;
using System.Diagnostics.Eventing.Reader;



namespace JFjewelery.Services
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICustomerService _customerService;

        public ChatSessionService(AppDbContext dbContext, ICustomerService customerService)
        {
            _dbContext = dbContext;
            _customerService = customerService;
        }


        // Customer is guaranteed to exist here due to prior GetOrCreateCustomerAsync
        public async Task<ChatSession> GetOrCteateSessionAsync(Update update)
        {
            var telegramAcc = update.Message?.From?.Username
               ?? update.CallbackQuery?.From?.Username
               ?? update.Message?.From?.Id.ToString()
               ?? update.CallbackQuery?.From?.Id.ToString();

            var chatId = update.GetChatId();
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            if (customer == null && telegramAcc != null)
            {
                
                customer = await _customerService.GetOrCreateCustomerAsync(chatId, telegramAcc);
            }

            //Null check
            if (customer == null)
                throw new Exception("Customer could not be resolved. ChatSessionService_1");

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);
            if (customer != null && session == null)
            {
                session = new ChatSession
                {
                    CustomerId = customer.Id,
                    CurrentScenario = null,
                    ScenarioStep = null,
                    FilterJson = null,
                    TempData = null,
                    LastUpdated = DateTime.UtcNow
                };
                _dbContext.ChatSessions.Add(session);
                await _dbContext.SaveChangesAsync();
            }

            //Null check
            if (session == null)
                throw new Exception("Session could not be resolved.");

            return session;
        }

        public async Task UpdateSessionAsync(ChatSession session)
        {
            session.LastUpdated = DateTime.UtcNow;
            _dbContext.ChatSessions.Update(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ResetSessionAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            //Null check
            if (customer == null)
                throw new Exception("Customer could not be resolved.ChatSessionService_2");


            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);
            if(customer != null &&  session != null)
            {
                session.CustomerId = customer.Id;
                session.CurrentScenario = null;
                session.ScenarioStep = null;
                session.FilterJson = null;
                session.TempData = null;
                session.LastUpdated = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }
            else if (customer != null)
            {
                session = new ChatSession
                {
                    CustomerId = customer.Id,
                    CurrentScenario = null,
                    ScenarioStep = null,
                    FilterJson = null,
                    TempData = null,
                    LastUpdated = DateTime.UtcNow
                };
                _dbContext.ChatSessions.Add(session);
                await _dbContext.SaveChangesAsync();
            }
        }

        //Filter characteristics methods
        public async Task<ProductFilterCriteria> GetFilterCriteriaAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            //Null check
            if (customer == null)
                throw new Exception("Customer could not be resolved.ChatSessionService_3");

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id)
                  ?? throw new Exception("Chat session not found");

            ProductFilterCriteria filterFromSession;

            if (session == null || string.IsNullOrEmpty(session.FilterJson))
            {
                filterFromSession = new ProductFilterCriteria();
            }
            else
            {
                filterFromSession = JsonSerializer.Deserialize<ProductFilterCriteria>(session.FilterJson) ?? new ProductFilterCriteria();
            }

            return filterFromSession;
        }

        public async Task ResetFilterCriteriaAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            //Null check
            if (customer == null)
                throw new Exception("Customer could not be resolved.ChatSessionService_4");

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id)
                  ?? throw new Exception("Chat session not found");

            session.FilterJson = null;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFilterCriteriaAsync(long chatId, ProductFilterCriteria newCriteria, FilterOperation operation)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            //Null check
            if (customer == null)
                throw new Exception("Customer could not be resolved.ChatSessionService_5");


            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id)
                  ?? throw new Exception("Chat session not found");

            var existingCriteria = await GetOrCreateExistingCriteriaAsync(chatId);

            UpdateGeneral(existingCriteria, newCriteria, operation);
            UpdateMetals(existingCriteria, newCriteria, operation);
            UpdateStones(existingCriteria, newCriteria, operation);

            session.FilterJson = JsonSerializer.Serialize(existingCriteria);

            await _dbContext.SaveChangesAsync();
        }

        public void UpdateGeneral(ProductFilterCriteria target, ProductFilterCriteria source, FilterOperation operation)
        {
            target.Gender = source.Gender;
            target.Description = source.Description;

            if (operation == FilterOperation.Add)
            {
                target.Styles = MergeLists(target.Styles, source.Styles);
                target.Manufacturers = MergeLists(target.Manufacturers, source.Manufacturers);
            }
            else
            {
                target.Styles = RemoveLists(target.Styles, source.Styles);
                target.Manufacturers = RemoveLists(target.Manufacturers, source.Manufacturers);
            }
        }

        public void UpdateMetals(ProductFilterCriteria target, ProductFilterCriteria source, FilterOperation operation)
        {
            if (operation == FilterOperation.Add)
            {
                target.Metals = MergeLists(target.Metals, source.Metals);
                target.MetalShapes = MergeLists(target.MetalShapes, source.MetalShapes);
                target.MetalColors = MergeLists(target.MetalColors, source.MetalColors);
                target.MetalSizes = MergeLists(target.MetalSizes, source.MetalSizes);
                target.MetalTypes = MergeLists(target.MetalTypes, source.MetalTypes);
            }
            else
            {
                target.Metals = RemoveLists(target.Metals, source.Metals);
                target.MetalShapes = RemoveLists(target.MetalShapes, source.MetalShapes);
                target.MetalColors = RemoveLists(target.MetalColors, source.MetalColors);
                target.MetalSizes = RemoveLists(target.MetalSizes, source.MetalSizes);
                target.MetalTypes = RemoveLists(target.MetalTypes, source.MetalTypes);
            }

            target.Purity = source.Purity;
            if (source.WeightMin != null && source.WeightMax != null)
            {
                if (source.WeightMax >= source.WeightMin)
                {
                    target.WeightMin = source.WeightMin;
                    target.WeightMax = source.WeightMax;
                }
                else
                {
                    throw new ArgumentException("The minimum weight cannot be greater than the maximum.");
                }  
            }
            else
            {
                if (source.WeightMin != null)
                    target.WeightMin = source.WeightMin;

                if (source.WeightMax != null)
                    target.WeightMax = source.WeightMax;
            }
        }

        public void UpdateStones(ProductFilterCriteria target, ProductFilterCriteria source, FilterOperation operation)
        {
            if (operation == FilterOperation.Add)
            {
                target.Stones = MergeLists(target.Stones, source.Stones);
                target.StoneShapes = MergeLists(target.StoneShapes, source.StoneShapes);
                target.StoneColors = MergeLists(target.StoneColors, source.StoneColors);
                target.StoneSizes = MergeLists(target.StoneSizes, source.StoneSizes);
                target.StoneTypes = MergeLists(target.StoneTypes, source.StoneTypes);
            }
            else
            {
                target.Stones = RemoveLists(target.Stones, source.Stones);
                target.StoneShapes = RemoveLists(target.StoneShapes, source.StoneShapes);
                target.StoneColors = RemoveLists(target.StoneColors, source.StoneColors);
                target.StoneSizes = RemoveLists(target.StoneSizes, source.StoneSizes);
                target.StoneTypes = RemoveLists(target.StoneTypes, source.StoneTypes);
            }

            if (source.CountMin != null && source.CountMax != null)
            {
                if (source.CountMax >= source.CountMin)
                {
                    target.CountMin = source.CountMin;
                    target.CountMax = source.CountMax;
                }
                else
                {
                    throw new ArgumentException("The minimum number of stones cannot be greater than the maximum.");
                }
            }
            else
            {
                if (source.CountMin != null)
                    target.CountMin = source.CountMin;

                if (source.CountMax != null)
                    target.CountMax = source.CountMax;
            }


        }


        public async Task<ProductFilterCriteria> GetOrCreateExistingCriteriaAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            //Null check
            if (customer == null)
                throw new Exception("Customer could not be resolved.ChatSessionService_6");

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);

            if (session == null)
                throw new Exception("Session could not be resolved.");

            //Create new criteria if doesn't exists
            ProductFilterCriteria existingCriteria = new ProductFilterCriteria();
            if (string.IsNullOrEmpty(session.FilterJson))
            {
                var newCriteria = new ProductFilterCriteria();
                session.FilterJson = JsonSerializer.Serialize(newCriteria);
                await _dbContext.SaveChangesAsync();
                return newCriteria;
            }

            //Desirialize existing criteria
            existingCriteria = JsonSerializer.Deserialize<ProductFilterCriteria>(session.FilterJson) ?? new ProductFilterCriteria();

            return existingCriteria;
        }

        //Lists changings
        public List<string> MergeLists(List<string>? original, List<string>? incoming)
        {
            var set = new HashSet<string>(original ?? new List<string>());
            if (incoming != null)
            {
                foreach (var item in incoming)
                    set.Add(item);
            }
            return set.ToList();
        }

        public List<string> RemoveLists(List<string>? original, List<string>? incoming)
        {
            var set = new HashSet<string>(original ?? new List<string>());
            if (incoming != null)
            {
                foreach (var item in incoming)
                    set.Remove(item);
            }
            return set.ToList();
        }



    }
}
