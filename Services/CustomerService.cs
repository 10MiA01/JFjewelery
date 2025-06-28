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


        public async Task<Customer> GetOrCreateCustomerAsync(long chatId, string telegramAcc)
        {
            var customer = await _dbContext.Customers
                .Include(c => c.ChatSession)
                .FirstOrDefaultAsync(c => c.ChatId == chatId);


            if (customer == null)
            {
                customer = new Customer 
                { 
                    ChatId = chatId , 
                    TelegramAcc = telegramAcc, 
                    Name = "Unknown" ,
                    ChatSession = new ChatSession
                    {
                        LastUpdated = DateTime.UtcNow
                    }
                };
                _dbContext.Customers.Add(customer);
                await _dbContext.SaveChangesAsync();
            }
            else if (customer.ChatSession == null)
            {
                customer.ChatSession = new ChatSession
                {
                    CustomerId = customer.Id,
                    LastUpdated = DateTime.UtcNow,
                };
                _dbContext.ChatSessions.Add(customer.ChatSession);
                await _dbContext.SaveChangesAsync();
            }

            return customer;
        }


        // Customer is guaranteed to exist here due to prior GetOrCreateCustomerAsync
        public async Task<ChatSession> GetChatSessionAsync(long chatId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            return await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customer.Id);
        }




    }
}
