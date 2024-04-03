using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace CompanyProfile.Models
{
    public class CompanyReview
    {
        [Required]
        [ForeignKey("Company")]
        public string CompanyEmail { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public int Rating { get; set; }

        [JsonIgnore]
        public virtual Company? Company { get; set; }
    }
}
