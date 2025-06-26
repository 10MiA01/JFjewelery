using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models.Characteristics
{
    public class CharacteristicMetal
    {
        public int CharacteristicId { get; set; }
        public Characteristic Characteristic { get; set; } = null!;

        public int MetalId { get; set; }
        public Metal Metal { get; set; } = null!;

        public int? ShapeId { get; set; }
        public Shape? Shape { get; set; }

        public int? ColorId { get; set; }
        public Color? Color { get; set; }

        public int? SizeId { get; set; }
        public Size? Size { get; set; }
        public int? TypeId { get; set; }
        public JType? Type { get; set; }

        public int? Purity { get; set; }

        public float? Weight { get; set; }
    }
}
