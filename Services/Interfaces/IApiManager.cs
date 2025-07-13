using JFjewelery.Models.DTO;

namespace JFjewelery.Services.Interfaces
{
    public interface IApiManager
    {
        Task<ProductFilterCriteria> AnalyzeImageAsync(byte[] imageData, CancellationToken cancellationToken = default);
    }
}
