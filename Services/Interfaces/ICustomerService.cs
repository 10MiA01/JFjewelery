using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models;

namespace JFjewelery.Services.Interfaces
{
    public interface ICustomerService

    {

        Task<Customer> GetOrCreateCustomerAsync(string telegramAcc);
        Task<ChatSession> GetChatSessionAsync(int customerId);
        Task SaveChatSessionAsync(int customerId, ChatSession state);

    }
}
