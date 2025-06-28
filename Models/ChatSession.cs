using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models
{
    public class ChatSession
    {

        [Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;

        public string? CurrentScenario { get; set; } 

        public string? ScenarioStep { get; set; }

        public string? FilterJson { get; set; }

        public string? TempData { get; set; }

        public DateTime? LastUpdated { get; set; }

    }
}
