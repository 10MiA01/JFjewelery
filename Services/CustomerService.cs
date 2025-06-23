using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Data;
using JFjewelery.Models;
using JFjewelery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

namespace JFjewelery.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly AppDbContext _dbContext;

        public CustomerService(ITelegramBotClient botClient, AppDbContext dbContext)
        {
            _botClient = botClient;
            _dbContext = dbContext;
        }
        public async Task<Customer> GetOrCreateCustomerAsync(string telegramAcc)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.TelegramAcc == telegramAcc);
            if (customer == null)
            {
                customer = new Customer { TelegramAcc = telegramAcc, Name = "Unknown" };
                _dbContext.Customers.Add(customer);
                await _dbContext.SaveChangesAsync();
            }
            return customer;
        }


        public async Task<ChatSession> GetChatSessionAsync(int customerId)
        {
            return await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customerId);
        }


        public async Task SaveChatSessionAsync(int customerId, ChatSession state)
        {
            var existingSession = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customerId);
            if (existingSession == null)
            {
                _dbContext.ChatSessions.Add(state);
            }
            else
            {
                existingSession.CurrentScenario = state.CurrentScenario;
                existingSession.ScenarioStep = state.ScenarioStep;
                existingSession.TempData = state.TempData;
                existingSession.LastUpdated = state.LastUpdated;
            }
        }

    }
}
