using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models.DTO
{
    public class ProductFilterCriteria
    {

        //General
        public string? Gender { get; set; }
        public List<string>? Styles { get; set; }
        public List<string>? Manufacturers { get; set; }
        public string? Description { get; set; }

        //For Metals
        public List<string>? Metals { get; set; }
        public List<string>? MetalShapes { get; set; }
        public List<string>? MetalColors { get; set; } 
        public List<string>? MetalSizes { get; set; }
        public List<string>? MetalTypes { get; set; }
        public int? Purity { get; set; }
        public float? WeightMin { get; set; }
        public float? WeightMax { get; set; }

        //For Stones
        public List<string>? Stones { get; set; } 
        public List<string>? StoneShapes { get; set; } 
        public List<string>? StoneColors { get; set; }
        public List<string>? StoneSizes { get; set; } 
        public List<string>? StoneTypes { get; set; } 
        public int? CountMin { get; set; }
        public int? CountMax { get; set; }

    }
}
