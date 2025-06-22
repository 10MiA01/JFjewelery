using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models
{
    public class CustomerPaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        [Required]
        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = null!;

        public string? Details { get; set; }
    }

}
