using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace JFjewelery.Extensions
{
    public static class UpdateExtensions
    {
        public static long GetChatId(this Update update)
        {
            if (update.CallbackQuery?.Message?.Chat?.Id != null)
                return update.CallbackQuery.Message.Chat.Id;

            if (update.Message?.Chat?.Id != null)
                return update.Message.Chat.Id;

            throw new InvalidOperationException("Unable to extract ChatId from update.");
        }
    }
}
