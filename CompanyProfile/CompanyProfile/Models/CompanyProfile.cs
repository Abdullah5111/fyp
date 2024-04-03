using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CompanyProfile.Models
{
    public class CompanyProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Company")]
        public string? CompanyEmail { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        //public string? Country { get; set; }

        [MaxLength]
        public byte[]? Logo { get; set; } // Store the logo as binary data

        [Display(Name = "Number of Employees")]
        public int? NumberOfEmployees { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [MaxLength(5000)] // Adjust the length as needed
        public string? Bio { get; set; }
        public string? Website { get; set; }
        public string? LinkedIn { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        // Navigation property to reference the associated Company
        [JsonIgnore]
        public virtual Company? Company { get; set; }
    }
}
