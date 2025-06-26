using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models.Characteristics
{
    public class CharacteristicStone
    {
        public int CharacteristicId { get; set; }
        public Characteristic Characteristic { get; set; } = null!;

        public int StoneId { get; set; }
        public Stone Stone { get; set; } = null!;

        public int? ShapeId { get; set; }
        public Shape? Shape { get; set; }

        public int? ColorId { get; set; }
        public Color? Color { get; set; }

        public int? SizeId { get; set; }
        public Size? Size { get; set; }
        public int? TypeId { get; set; }
        public JType? Type { get; set; }

        public int Count { get; set; }
    }
}
