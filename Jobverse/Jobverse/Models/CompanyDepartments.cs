using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jobverse.Models
{
    public class CompanyDepartments
    {
        [Required]
        [ForeignKey("Company")]
        public string CompanyEmail { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Department1 { get; set; }

        [Required]
        public string Department2 { get; set; }

        [Required]
        public string Role1 { get; set; }

        [Required]
        public double Salary1 { get; set; }

        [Required]
        public string Role2 { get; set; }

        [Required]
        public double Salary2 { get; set; }
        [Required]
        public string Role3 { get; set; }

        [Required]
        public double Salary3 { get; set; }
        [Required]
        public string Role4 { get; set; }

        [Required]
        public double Salary4 { get; set; }
        [Required]
        public string Role5 { get; set; }

        [Required]
        public double Salary5 { get; set; }
        [Required]
        public string Role6 { get; set; }

        [Required]
        public double Salary6 { get; set; }
        [Required]
        public string Role7 { get; set; }

        [Required]
        public double Salary7 { get; set; }
        [Required]
        public string Role8 { get; set; }

        [Required]
        public double Salary8 { get; set; }
        [Required]
        public string Role9 { get; set; }

        [Required]
        public double Salary9 { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
    }
}
