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
        public List<string> Styles { get; set; } = new();
        public List<string> Manufacturers { get; set; } = new();
        public string? Description { get; set; }

        //For Metals
        public List<string> Metals { get; set; } = new();
        public List<string> MetalShapes { get; set; } = new();
        public List<string> MetalColors { get; set; } = new();
        public List<string> MetalSizes { get; set; } = new();
        public List<string> MetalTypes { get; set; } = new();
        public int? Purity { get; set; }
        public float? WeightMin { get; set; }
        public float? WeightMax { get; set; }

        //For Stones
        public List<string> Stones { get; set; } = new();
        public List<string> StoneShapes { get; set; } = new();
        public List<string> StoneColors { get; set; } = new();
        public List<string> StoneSizes { get; set; } = new();
        public List<string> StoneTypes { get; set; } = new();
        public int? CountMin { get; set; }
        public int? CountMax { get; set; }

    }
}
