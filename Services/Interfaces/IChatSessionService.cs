using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Enums;
using static JFjewelery.Services.ChatSessionService;


namespace JFjewelery.Services.Interfaces
{
    public interface IChatSessionService
    {
        Task<ChatSession> GetOrCteateSessionAsync(long chatId);
        Task UpdateSessionAsync(ChatSession session);
        Task ResetSessionAsync(long chatId);
        Task<ProductFilterCriteria> GetFilterCriteriaAsync(long chatId);
        Task ResetFilterCriteriaAsync(long chatId);
        Task UpdateFilterCriteriaAsync(long chatId, ProductFilterCriteria newCriteria, FilterOperation operation);
        void UpdateGeneral(ProductFilterCriteria target, ProductFilterCriteria source, FilterOperation operation);
        void UpdateMetals(ProductFilterCriteria target, ProductFilterCriteria source, FilterOperation operation);
        void UpdateStones(ProductFilterCriteria target, ProductFilterCriteria source, FilterOperation operation);
        Task<ProductFilterCriteria> GetOrCreateExistingCriteriaAsync(long chatId);
        List<string> MergeLists(List<string>? original, List<string>? incoming);
        List<string> RemoveLists(List<string>? original, List<string>? incoming);

    }
}
