using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models.Characteristics;

namespace JFjewelery.Models
{
    public class Characteristic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public ICollection<CharacteristicStone>? Stones { get; set; } = new List<CharacteristicStone>();
        public ICollection<CharacteristicMetal>? Metals { get; set; } = new List<CharacteristicMetal>();

        [MaxLength(20)]
        public string? Gender { get; set; }     

        [MaxLength(30)]
        public string? Style { get; set; }      

        [MaxLength(50)]
        public string? Manufacturer { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
