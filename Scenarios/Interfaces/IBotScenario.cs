using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace JFjewelery.Scenarios.Interfaces
{
    public interface IBotScenario
    {
        string Name { get; }
        Task ExecuteAsync(Update update, CancellationToken cancellationToken);
    }
}
