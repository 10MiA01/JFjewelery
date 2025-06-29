﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Customer name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string TelegramAcc { get; set; }

        public long ChatId { get; set; }

        public ChatSession? ChatSession { get; set; } = null!;

        public ICollection<CustomerPaymentMethod> CustomerPaymentMethods { get; set; } = new List<CustomerPaymentMethod>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
