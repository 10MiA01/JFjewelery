using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models.Scenario;
using JFjewelery.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace JFjewelery.Services
{
    public class ButtonComposer : IButtonComposer
    {

        public InlineKeyboardMarkup CreateKeyboard(Step step)
        {
            var options = step.Options.ToList();
            var buttons = options
                    .Select(o => InlineKeyboardButton.WithCallbackData(o.Name, o.Name))
                    .Chunk(2)
                    .ToList();

            var finishButton = new[]
            {
                InlineKeyboardButton.WithCallbackData("Finish", "Finish")
            };

            buttons.Add(finishButton);

            var keyboard = new InlineKeyboardMarkup(buttons);

            return keyboard;
        }

        
    }
}
