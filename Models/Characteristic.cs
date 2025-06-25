using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [MaxLength(30)]
        public string? Material { get; set; }  

        public int? Purity { get; set; }       

        public float? Weight { get; set; }     

        [MaxLength(20)]
        public string? MetalColor { get; set; }  

        [MaxLength(50)]
        public string? Stone { get; set; }
        [MaxLength(50)]
        public string? StoneColor { get; set; }
        public int? StoneCount { get; set; }

        [MaxLength(30)]
        public string? StoneShape { get; set; } 

        [MaxLength(10)]
        public string? Size { get; set; }       

        [MaxLength(30)]
        public string? Coating { get; set; }    

        [MaxLength(20)]
        public string? Gender { get; set; }     

        [MaxLength(30)]
        public string? Style { get; set; }      

        [MaxLength(50)]
        public string? Manufacturer { get; set; }
    }
}
