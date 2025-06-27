using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFjewelery.Models.Characteristics;

namespace JFjewelery.Models.Scenario
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Content { get; set; }
        public string? FilterJson { get; set; }

        public int? StepId { get; set; }
        public Step? Step { get; set; }
    }
}
