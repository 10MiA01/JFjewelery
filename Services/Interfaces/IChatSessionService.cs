using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models;


namespace JFjewelery.Services.Interfaces
{
    internal interface IChatSessionService
    {
        Task<ChatSession> GetOrCteateSessionAsync(int customerId);
        Task UpdateSessionAsync(ChatSession session);
        Task ResetSessionAsync(int customerId);
    }
}
