using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [RegularExpression(@"^prod_(\d+)_([a-z]+)(?:_(\d+))?\.(jpg|png)$", ErrorMessage = "Invalid file name format.")]
        //prod_prodId_view_viewNr.format
        public string FileName { get; set; } = null!;

        [Required]
        public string FilePath => $"Media/images/products/{ProductId}/{FileName}";
    }
}
