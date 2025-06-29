using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JFjewelery.Data;
using JFjewelery.Models.DTO;
using JFjewelery.Models.Characteristics;
using JFjewelery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using JFjewelery.Models;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;

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
                .OrderByDescending(p = productMatches.Values)
                .Take(3)
                .Select(p => p.Key)
                .ToList();

            return top3Products;

        }

        public int MatchFilters(ProductFilterCriteria clientFilter, ProductFilterCriteria productFilter)
        {
            int matchScore = 0;

            if (clientFilter.Gender != null && clientFilter.Gender == productFilter.Gender)
                matchScore += 50;

            matchScore += productFilter.Styles?.Count(style => clientFilter.Styles?.Contains(style) == true) ?? 0;
            matchScore += productFilter.Manufacturers?.Count(manuf => clientFilter.Manufacturers?.Contains(manuf) == true) ?? 0;

            if (!string.IsNullOrEmpty(productFilter.Description) && clientFilter.Description?.Contains(productFilter.Description) == true)
                matchScore += 10;

            matchScore += productFilter.Metals?.Count(m => clientFilter.Metals?.Contains(m) == true) ?? 0;
            matchScore += productFilter.MetalShapes?.Count(s => clientFilter.MetalShapes?.Contains(s) == true) ?? 0;
            matchScore += productFilter.MetalColors?.Count(c => clientFilter.MetalColors?.Contains(c) == true) ?? 0;
            matchScore += productFilter.MetalSizes?.Count(s => clientFilter.MetalSizes?.Contains(s) == true) ?? 0;
            matchScore += productFilter.MetalTypes?.Count(t => clientFilter.MetalTypes?.Contains(t) == true) ?? 0;

            if (productFilter.Purity != null && clientFilter.Purity == productFilter.Purity)
                matchScore++;

            if (productFilter.WeightMin != null && clientFilter.WeightMin != null && clientFilter.WeightMin <= productFilter.WeightMin)
                matchScore++;

            if (productFilter.WeightMax != null && clientFilter.WeightMax != null && clientFilter.WeightMax >= productFilter.WeightMax)
                matchScore++;

            matchScore += productFilter.Stones?.Count(s => clientFilter.Stones?.Contains(s) == true) ?? 0;
            matchScore += productFilter.StoneShapes?.Count(s => clientFilter.StoneShapes?.Contains(s) == true) ?? 0;
            matchScore += productFilter.StoneColors?.Count(c => clientFilter.StoneColors?.Contains(c) == true) ?? 0;
            matchScore += productFilter.StoneSizes?.Count(s => clientFilter.StoneSizes?.Contains(s) == true) ?? 0;
            matchScore += productFilter.StoneTypes?.Count(t => clientFilter.StoneTypes?.Contains(t) == true) ?? 0;

            if (productFilter.CountMin != null && clientFilter.CountMin != null && clientFilter.CountMin <= productFilter.CountMin)
                matchScore++;

            if (productFilter.CountMax != null && clientFilter.CountMax != null && clientFilter.CountMax >= productFilter.CountMax)
                matchScore++;

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


                Manufacturers = string.IsNullOrEmpty(characteristic.Manufacturer)
                ? new List<string>()
                    : new List<string> { characteristic.Manufacturer },


                Description = product.Characteristic.Description,

                //For Metals
                Metals = characteristic?.Metals?
                        .Select(m => m.Metal.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                .ToList(),



                MetalShapes = characteristic?.Metals?
                        .Select(m => m.Shape.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                .ToList(),

                MetalColors = characteristic?.Metals?
                        .Select(m => m.Color.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                .ToList(),

                MetalSizes = characteristic?.Metals?
                        .Select(m => m.Size.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                MetalTypes = characteristic?.Metals?
                        .Select(m => m.Type.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                Purity = characteristic?.Metals?
                    .Where(m => m.Purity.HasValue)
                    .Select(m => m.Purity.Value)
                    .FirstOrDefault(),



                WeightMin = characteristic?.Metals?
                        .Where(m => m.Weight.HasValue)
                        .Min(m => m.Weight),

                WeightMax = characteristic?.Metals?
                        .Where(m => m.Weight.HasValue)
                        .Max(m => m.Weight),

                //For Stones
                Stones = characteristic?.Stones?
                        .Select(s => s.Stone?.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                StoneShapes = characteristic?.Stones?
                        .Select(s => s.Shape?.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                StoneColors = characteristic?.Stones?
                        .Select(s => s.Color?.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                StoneSizes = characteristic?.Stones?
                        .Select(s => s.Size?.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                StoneTypes = characteristic?.Stones?
                        .Select(s => s.Type?.Name)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .Distinct()
                        .ToList(),

                CountMin = characteristic?.Stones?.Min(s => s.Count),
                CountMax = characteristic?.Stones?.Max(s => s.Count)
            };

            return productFilter;
        }


    }
}
