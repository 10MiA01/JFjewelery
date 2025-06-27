using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models;


namespace JFjewelery.Services.Interfaces
{
    public interface IChatSessionService
    {
        Task<ChatSession> GetOrCteateSessionAsync(long chatId);
        Task UpdateSessionAsync(ChatSession session);
        Task ResetSessionAsync(int customerId);
    }
}
