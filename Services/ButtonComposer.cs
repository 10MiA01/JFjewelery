using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models.Enums;
using JFjewelery.Models.Scenario;
using JFjewelery.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace JFjewelery.Services
{
    public class ButtonComposer : IButtonComposer
    {

        public InlineKeyboardMarkup CreateKeyboard(Step step, ExtraButtonType extraButtons = ExtraButtonType.None)
        {
            var options = step.Options?.ToList() ?? new List<Option>();
            var buttons = options
                    .Select(o => InlineKeyboardButton.WithCallbackData(o.Name, o.Name))
                    .Chunk(2)
                    .ToList();


            if (extraButtons == ExtraButtonType.None)
            {
                var keyboard = new InlineKeyboardMarkup(buttons);
               return keyboard;
            }
            else
            {
                var addButtons = AddExtraButtons(extraButtons);
                buttons.Add(addButtons);
                var keyboard = new InlineKeyboardMarkup(buttons);
                return keyboard;
            }

        }

        public InlineKeyboardButton[] AddExtraButtons(ExtraButtonType extraButtons)
        {
            if (extraButtons == ExtraButtonType.Finish)
            {
                var finishButton = new[] {InlineKeyboardButton.WithCallbackData("Finish", "Finish")};
                return finishButton;
            }
            else if (extraButtons == ExtraButtonType.Cancel)
            {
                var cancelButton = new[] { InlineKeyboardButton.WithCallbackData("Cancel", "Cancel") };
                return cancelButton;
            }
            else if (extraButtons == ExtraButtonType.FinishAndCancel)
            {
                var finishCancelButton = new[] 
                {
                    InlineKeyboardButton.WithCallbackData("Finish", "Finish"),
                    InlineKeyboardButton.WithCallbackData("Cancel", "Cancel") 
                };
                return finishCancelButton;
            }
            else
            {
                return Array.Empty<InlineKeyboardButton>(); 
            }
        }

        //public InlineKeyboardMarkup CreateFromCategories()
        //{

        //}


    }
}
