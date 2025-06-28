using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFjewelery.Models.Scenario
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string QuestionText { get; set; }

        public int? NextStepId { get; set; }
        public Step? NextStep { get; set; }

        public int ScenarioId { get; set; }
        public Scenario Scenario { get; set; }

        public List<Option> Options { get; set; } = new();
    }
}
