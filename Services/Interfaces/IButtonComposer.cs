using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models.Scenario;
using Telegram.Bot.Types.ReplyMarkups;

namespace JFjewelery.Services.Interfaces
{
    public interface IButtonComposer
    {
        InlineKeyboardMarkup CreateKeyboard(Step step);
    }
}
