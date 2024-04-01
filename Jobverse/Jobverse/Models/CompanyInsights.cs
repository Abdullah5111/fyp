using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jobverse.Models
{
    public class CompanyInsights
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Company")]
        public string? CompanyEmail { get; set; }
        public DateTime? EstablishedSince { get; set; }
        public int? OrdersCompleted { get; set; }
        public double? EstimatedRevenue { get; set; }
        public int? ProductsSold { get; set; }
        public int? SatisfiedCustomers { get; set; }
        public double? CustomerGrowthPercentage { get; set; }
        // Navigation property to reference the associated Company
        [JsonIgnore]
        public virtual Company? Company { get; set; }
    }
}
