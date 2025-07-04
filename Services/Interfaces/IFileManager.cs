using Telegram.Bot;
using Telegram.Bot.Types;

namespace JFjewelery.Services.Interfaces
{
    public interface IFileManager
    {
        Task<byte[]?> DownloadPhotoAsync(Update update, CancellationToken cancellationToken);
    }
}
