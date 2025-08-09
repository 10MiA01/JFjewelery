using JFjewelery.Models.DTO;

namespace JFjewelery.Services.Interfaces
{
    public interface IApiManager
    {
        Task<ProductFilterCriteria> AnalyzeImageAsync(byte[] imageData, CancellationToken cancellationToken = default);

        Task<byte[]> TryOnAsync(byte[] imageData, string category, int id, CancellationToken cancellationToken = default);

        Task<ProductFilterCriteria> AnalyzeSentenceAsync(string description, CancellationToken cancellationToken = default);
    }
}
