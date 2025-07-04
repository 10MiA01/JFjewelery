using System.Threading;
using JFjewelery.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace JFjewelery.Services
{
    public class FileManager : IFileManager
    {
        private readonly ITelegramBotClient _botClient;

        public FileManager(ITelegramBotClient botClient) 
        {
            _botClient = botClient;
        }

        public async Task<byte[]?> DownloadPhotoAsync(Update update, CancellationToken cancellationToken)
        {
            string? fileId = null;


            //Get the picture
            if (update.Message.Photo != null && update.Message.Photo.Any())
            {
                fileId = update.Message.Photo.Last().FileId;
            }
            //Get the picture as a document
            else if (update.Message.Document != null && update.Message.Document.MimeType.StartsWith("image/"))
            {
                fileId = update.Message.Document.FileId;
            }
            //Null check
            if (fileId == null)
                return null;

            var file = await _botClient.GetFileAsync(fileId, cancellationToken);
            var filePath = file.FilePath;

            using var stream = new MemoryStream();
            await _botClient.DownloadFileAsync(filePath, stream, cancellationToken);
            return stream.ToArray(); // return as byte[]
        }
    }
}
