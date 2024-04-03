using System.ComponentModel.DataAnnotations;

namespace ApplyForJob.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string Applier { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid experience.")]
        public int? Experience { get; set; }

        [Required]
        public DateTime ApplyTime { get; set; } = DateTime.UtcNow;

        [Required]
        public int ResumeId { get; set; }

        public JobApplication()
        {
            UserEmail = string.Empty;
            Applier = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
