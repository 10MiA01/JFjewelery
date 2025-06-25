using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Data;
using JFjewelery.Models;
using JFjewelery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JFjewelery.Services
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly AppDbContext _dbContext;

        public ChatSessionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChatSession> GetOrCteateSessionAsync(int customerId)
        {
            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customerId);
            if (session == null)
            {
                session = new ChatSession
                {
                    CustomerId = customerId,
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

        public async Task ResetSessionAsync(int customerId)
        {
            var session = await _dbContext.ChatSessions.FirstOrDefaultAsync(s => s.CustomerId == customerId);
            if(session != null)
            {
                session.CustomerId = customerId;
                session.CurrentScenario = null;
                session.ScenarioStep = null;
                session.TempData = null;
                session.LastUpdated = DateTime.UtcNow;
            }
            else
            {
                session = new ChatSession
                {
                    CustomerId = customerId,
                    CurrentScenario = "Idle",
                    LastUpdated = DateTime.UtcNow
                };
                _dbContext.ChatSessions.Add(session);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
