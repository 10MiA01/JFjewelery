using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using JFjewelery.Data;
using JFjewelery.Models;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Enums;
using JFjewelery.Services.Interfaces;


namespace JFjewelery.Services
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly AppDbContext _dbContext;

        public ChatSessionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Customer is guaranteed to exist here due to prior GetOrCreateCustomerAsync
        public async Task<ChatSession> GetOrCteateSessionAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);
            if (session == null)
            {
                session = new ChatSession
                {
                    CustomerId = customer.Id,
                    CurrentScenario = "Idle",
                    LastUpdated = DateTime.UtcNow
                };
                _dbContext.ChatSessions.Add(session);
                await _dbContext.SaveChangesAsync();
            }
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


            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);
            if(session != null)
            {
                session.CustomerId = customer.Id;
                session.CurrentScenario = null;
                session.ScenarioStep = null;
                session.FilterJson = null;
                session.TempData = null;
                session.LastUpdated = DateTime.UtcNow;
            }
            else
            {
                session = new ChatSession
                {
                    CustomerId = customer.Id,
                    CurrentScenario = "Idle",
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

            if (customer == null)
                throw new Exception("Customer not found");

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id)
                  ?? throw new Exception("Chat session not found");

            ProductFilterCriteria filterFromSession = JsonSerializer.Deserialize<ProductFilterCriteria>(session.FilterJson) ?? new ProductFilterCriteria();

            return filterFromSession;
        }

        public async Task ResetFilterCriteriaAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            if (customer == null)
                throw new Exception("Customer not found");

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id)
                  ?? throw new Exception("Chat session not found");

            session.FilterJson = null;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFilterCriteriaAsync(long chatId, ProductFilterCriteria newCriteria, FilterOperation operation)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);


            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id)
                  ?? throw new Exception("Chat session not found");

            var existingCriteria = await GetOrCreateExistingCriteriaAsync(customer.Id);

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
            if (source.WeightMax > source.WeightMin)
            {
                target.WeightMin = source.WeightMin;
                target.WeightMax = source.WeightMax;
            }
            else
            {
                throw new ArgumentException("The minimum weight cannot be greater than the maximum.");
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

            if (source.CountMax > source.CountMin)
            {
                target.CountMin = source.CountMin;
                target.CountMax = source.CountMax;
            }
            else
            {
                throw new ArgumentException("The minimum number of stones cannot be greater than the maximum.");
            }

            
        }


        public async Task<ProductFilterCriteria> GetOrCreateExistingCriteriaAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);

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
