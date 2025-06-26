using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models.Characteristics
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? AppliesToEntityId { get; set; }
        public AppliesToEntity? AppliesTo { get; set; }
    }
}
