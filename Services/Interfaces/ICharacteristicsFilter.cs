using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models;
using JFjewelery.Models.DTO;

namespace JFjewelery.Services.Interfaces
{
    public interface ICharacteristicsFilter
    {
        Task<List<Product>> FilterMatchProductsAsync(ProductFilterCriteria productFilterCriteria);
        Task<List<Product>> FilterSelectProductsAsync(ProductFilterCriteria clientFilter);
        bool SelectFilters(ProductFilterCriteria clientFilter, ProductFilterCriteria productFilter);
        int MatchFilters(ProductFilterCriteria clientFilter, ProductFilterCriteria productFilter);
        ProductFilterCriteria ConvertProductToCriteria(Product product);


    }
}
