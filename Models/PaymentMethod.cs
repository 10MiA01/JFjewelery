using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Payment method")]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<CustomerPaymentMethod> CustomerPaymentMethods { get; set; } = new List<CustomerPaymentMethod>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
