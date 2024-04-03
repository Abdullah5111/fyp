using System;
using System.ComponentModel.DataAnnotations;

namespace Jobverse.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string Applier { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public bool Status { get; set; } = false;

        [Required]
        public DateTime Time { get; set; }

        public Notification()
        {
            UserEmail = string.Empty;
        }
    }
}
