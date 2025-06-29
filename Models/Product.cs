using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Price { get; set; }
        public Characteristic Characteristic { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();


    }
}
