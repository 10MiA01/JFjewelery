using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JFjewelery.Data;
using JFjewelery.Models;
using JFjewelery.Models.Characteristics;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Helpers;
using JFjewelery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using static System.Formats.Asn1.AsnWriter;

namespace JFjewelery.Services
{
    public class CharacteristicsFilter : ICharacteristicsFilter
    {
        private readonly AppDbContext _dbContext;

        public CharacteristicsFilter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public async Task<List<Product>> FilterMatchProductsAsync(ProductFilterCriteria clientFilter)
        {
            Dictionary<Product, int> productMatches = new Dictionary<Product, int>();

            var products = await _dbContext.Products
                .Include(p => p.Characteristic)
                .Include(p => p.Images)
                .ToListAsync();

            foreach (var product in products)
            {
                var characteristic = product.Characteristic;

                await _dbContext.Entry(characteristic)
                    .Collection(c => c.Stones).Query()
                    .Include(s => s.Stone)
                    .Include(s => s.Shape)
                    .Include(s => s.Color)
                    .Include(s => s.Size)
                    .Include(s => s.Type)
                    .LoadAsync();

                await _dbContext.Entry(characteristic)
                    .Collection(c => c.Metals).Query()
                    .Include(m => m.Metal)
                    .Include(m => m.Shape)
                    .Include(m => m.Color)
                    .Include(m => m.Size)
                    .Include(m => m.Type)
                    .LoadAsync();
            }

            foreach (var product in products)
            {
                var productFilter = ConvertProductToCriteria(product);

                int score = MatchFilters(clientFilter, productFilter);

                productMatches.Add(product, score);
            }

            var top3Products = productMatches
                .OrderByDescending(p => p.Value)
                .Take(3)
                .Select(p => p.Key)
                .ToList();

            return top3Products;

        }

        public async Task<List<Product>> FilterSelectProductsAsync(ProductFilterCriteria clientFilter)
        {
            Dictionary<Product, int> productMatches = new Dictionary<Product, int>();

            var products = await _dbContext.Products
                .Include(p => p.Characteristic)
                .Include(p => p.Images)
                .ToListAsync();

            foreach (var product in products)
            {
                var characteristic = product.Characteristic;

                await _dbContext.Entry(characteristic)
                    .Collection(c => c.Stones).Query()
                    .Include(s => s.Stone)
                    .Include(s => s.Shape)
                    .Include(s => s.Color)
                    .Include(s => s.Size)
                    .Include(s => s.Type)
                    .LoadAsync();

                await _dbContext.Entry(characteristic)
                    .Collection(c => c.Metals).Query()
                    .Include(m => m.Metal)
                    .Include(m => m.Shape)
                    .Include(m => m.Color)
                    .Include(m => m.Size)
                    .Include(m => m.Type)
                    .LoadAsync();
            }
            foreach (var product in products)
            {
                var productFilter = ConvertProductToCriteria(product);

                if (SelectFilters(clientFilter, productFilter))
                {
                    int score = MatchFilters(clientFilter, productFilter);
                    productMatches.Add(product, score);
                }
            }

            var topProducts = productMatches
                .OrderByDescending(p => p.Value)
                .Take(3)
                .Select(p => p.Key)
                .ToList();

            return topProducts;
        }

        public async Task<Product> GetProductByCategoryAndFilter (string category, ProductFilterCriteria clientFilter)
        {
            var products = await _dbContext.Products
                .Include(p => p.Characteristic)
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Where(c => c.Category.Name == category)
                .ToListAsync();

            Dictionary<Product, int> productMatches = new Dictionary<Product, int>();
            foreach (var product in products)
            {
                var productFilter = ConvertProductToCriteria(product);

                int score = MatchFilters(clientFilter, productFilter);

                productMatches.Add(product, score);
            }

            var topProduct = productMatches
                .OrderByDescending(p => p.Value)
                .Select(p => p.Key)
                .FirstOrDefault(); ;

            return topProduct;
        }

        public bool SelectFilters(ProductFilterCriteria clientFilter, ProductFilterCriteria productFilter)
        {
            if (clientFilter.Gender != null && clientFilter.Gender != productFilter.Gender) return false;
            if (clientFilter.Styles != null &&
                (productFilter.Styles == null ||
                 !productFilter.Styles.Any(style => clientFilter.Styles.Contains(style)))) return false;
            if (clientFilter.Manufacturers != null &&
                (productFilter.Manufacturers == null ||
                 !productFilter.Manufacturers.Any(style => clientFilter.Manufacturers.Contains(style)))) return false;

            if (!string.IsNullOrEmpty(clientFilter.Description) &&
                !string.IsNullOrEmpty(productFilter.Description))
            {
                var clientWords = clientFilter.Description.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var productDescription = productFilter.Description.ToLower();

                bool hasMatch = clientWords.Any(word => productDescription.Contains(word));

                if (!hasMatch)
                    return false;
            }


            if (clientFilter.Metals != null &&
                (productFilter.Metals == null ||
                 !productFilter.Metals.Any(style => clientFilter.Metals.Contains(style)))) return false;
            if (clientFilter.MetalShapes != null &&
                    (productFilter.MetalShapes == null ||
                     !productFilter.MetalShapes.Any(style => clientFilter.MetalShapes.Contains(style)))) return false;
            if (clientFilter.MetalColors != null &&
                (productFilter.MetalColors == null ||
                 !productFilter.MetalColors.Any(style => clientFilter.MetalColors.Contains(style)))) return false;
            if (clientFilter.MetalSizes != null &&
                (productFilter.MetalSizes == null ||
                 !productFilter.MetalSizes.Any(style => clientFilter.MetalSizes.Contains(style)))) return false;
            if (clientFilter.MetalTypes != null &&
                (productFilter.MetalTypes == null ||
                 !productFilter.MetalTypes.Any(style => clientFilter.MetalTypes.Contains(style)))) return false;

            if (clientFilter.Purity != null && clientFilter.Purity != productFilter.Purity) return false;
            if (clientFilter.WeightMin != null && clientFilter.WeightMin <= productFilter.WeightMin) return false;
            if (clientFilter.WeightMax != null && clientFilter.WeightMax >= productFilter.WeightMax) return false;

            if (clientFilter.Stones != null &&
                (productFilter.Stones == null ||
                 !productFilter.Stones.Any(style => clientFilter.Stones.Contains(style)))) return false;
            if (clientFilter.StoneShapes != null &&
                    (productFilter.StoneShapes == null ||
                     !productFilter.StoneShapes.Any(style => clientFilter.StoneShapes.Contains(style)))) return false;
            if (clientFilter.StoneColors != null &&
                (productFilter.StoneColors == null ||
                 !productFilter.StoneColors.Any(style => clientFilter.StoneColors.Contains(style)))) return false;
            if (clientFilter.StoneSizes != null &&
                (productFilter.StoneSizes == null ||
                 !productFilter.StoneSizes.Any(style => clientFilter.StoneSizes.Contains(style)))) return false;
            if (clientFilter.StoneTypes != null &&
                (productFilter.StoneTypes == null ||
                 !productFilter.StoneTypes.Any(style => clientFilter.StoneTypes.Contains(style)))) return false;

            if (clientFilter.CountMin != null && clientFilter.CountMin <= productFilter.CountMin) return false;
            if (clientFilter.CountMax != null && clientFilter.CountMax >= productFilter.CountMax) return false;

            return true;
        }

        public int MatchFilters(ProductFilterCriteria clientFilter, ProductFilterCriteria productFilter)
        {
            int matchScore = 0;

            if (clientFilter.Gender != null && clientFilter.Gender == productFilter.Gender)
                matchScore += ProductFilterScore.Weights["Gender"];

            matchScore += (productFilter.Styles?.Count(style => clientFilter.Styles?.Contains(style) == true) ?? 0) * ProductFilterScore.Weights["Style"];
            matchScore += (productFilter.Manufacturers?.Count(manuf => clientFilter.Manufacturers?.Contains(manuf) == true) ?? 0) * ProductFilterScore.Weights["Manufacturer"];

            if (!string.IsNullOrEmpty(productFilter.Description) && clientFilter.Description?.Contains(productFilter.Description) == true)
                matchScore += ProductFilterScore.Weights["Description"];

            matchScore += (productFilter.Metals?.Count(m => clientFilter.Metals?.Contains(m) == true) ?? 0) * ProductFilterScore.Weights["Metal"];
            matchScore += (productFilter.MetalShapes?.Count(s => clientFilter.MetalShapes?.Contains(s) == true) ?? 0) * ProductFilterScore.Weights["MetalShape"];
            matchScore += (productFilter.MetalColors?.Count(c => clientFilter.MetalColors?.Contains(c) == true) ?? 0) * ProductFilterScore.Weights["MetalColor"];
            matchScore += (productFilter.MetalSizes?.Count(s => clientFilter.MetalSizes?.Contains(s) == true) ?? 0) * ProductFilterScore.Weights["MetalSize"];
            matchScore += (productFilter.MetalTypes?.Count(t => clientFilter.MetalTypes?.Contains(t) == true) ?? 0) * ProductFilterScore.Weights["MetalType"];

            if (productFilter.Purity != null && clientFilter.Purity == productFilter.Purity)
                matchScore += ProductFilterScore.Weights["Purity"];

            if (productFilter.WeightMin != null && clientFilter.WeightMin != null && clientFilter.WeightMin <= productFilter.WeightMin)
                matchScore += ProductFilterScore.Weights["Weight"];

            if (productFilter.WeightMax != null && clientFilter.WeightMax != null && clientFilter.WeightMax >= productFilter.WeightMax)
                matchScore += ProductFilterScore.Weights["Weight"];

            matchScore += (productFilter.Stones?.Count(s => clientFilter.Stones?.Contains(s) == true) ?? 0) * ProductFilterScore.Weights["Stone"];
            matchScore += (productFilter.StoneShapes?.Count(s => clientFilter.StoneShapes?.Contains(s) == true) ?? 0) * ProductFilterScore.Weights["StoneShape"];
            matchScore += (productFilter.StoneColors?.Count(c => clientFilter.StoneColors?.Contains(c) == true) ?? 0) * ProductFilterScore.Weights["StoneColor"];
            matchScore += (productFilter.StoneSizes?.Count(s => clientFilter.StoneSizes?.Contains(s) == true) ?? 0) * ProductFilterScore.Weights["StoneSize"];
            matchScore += (productFilter.StoneTypes?.Count(t => clientFilter.StoneTypes?.Contains(t) == true) ?? 0) * ProductFilterScore.Weights["StoneType"];

            if (productFilter.CountMin != null && clientFilter.CountMin != null && clientFilter.CountMin <= productFilter.CountMin)
                matchScore += ProductFilterScore.Weights["Count"];

            if (productFilter.CountMax != null && clientFilter.CountMax != null && clientFilter.CountMax >= productFilter.CountMax)
                matchScore += ProductFilterScore.Weights["Count"];

            return matchScore;
        }


        public ProductFilterCriteria ConvertProductToCriteria(Product product)
        {
            var characteristic = product.Characteristic;

            var productFilter = new ProductFilterCriteria()
            {
                Gender = characteristic?.Gender,

                Styles = string.IsNullOrEmpty(characteristic?.Style)
                    ? new List<string>()
                    : new List<string> { characteristic.Style },

                Manufacturers = string.IsNullOrEmpty(characteristic?.Manufacturer)
                    ? new List<string>()
                    : new List<string> { characteristic.Manufacturer },

                Description = characteristic?.Description,

                // Metals
                Metals = characteristic?.Metals?
                    .Select(m => m.Metal?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                MetalShapes = characteristic?.Metals?
                    .Select(m => m.Shape?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                MetalColors = characteristic?.Metals?
                    .Select(m => m.Color?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                MetalSizes = characteristic?.Metals?
                    .Select(m => m.Size?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                MetalTypes = characteristic?.Metals?
                    .Select(m => m.Type?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                Purity = characteristic?.Metals?
                    .Where(m => m.Purity.HasValue)
                    .Select(m => m.Purity.Value)
                    .FirstOrDefault(),

                WeightMin = characteristic?.Metals?
                    .Where(m => m.Weight.HasValue)
                    .Min(m => m.Weight) ?? 0,

                WeightMax = characteristic?.Metals?
                    .Where(m => m.Weight.HasValue)
                    .Max(m => m.Weight) ?? 0,

                // Stones
                Stones = characteristic?.Stones?
                    .Select(s => s.Stone?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                StoneShapes = characteristic?.Stones?
                    .Select(s => s.Shape?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                StoneColors = characteristic?.Stones?
                    .Select(s => s.Color?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                StoneSizes = characteristic?.Stones?
                    .Select(s => s.Size?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                StoneTypes = characteristic?.Stones?
                    .Select(s => s.Type?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .ToList() ?? new List<string>(),

                CountMin = characteristic?.Stones?.Min(s => s.Count) ?? 0,
                CountMax = characteristic?.Stones?.Max(s => s.Count) ?? 0
            };

            return productFilter;
        }



    }
}
