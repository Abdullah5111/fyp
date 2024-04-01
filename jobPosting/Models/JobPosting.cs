using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobPosting.Models
{
    public class JobPosting
    {
        
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string SalaryRange { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Qualifications { get; set; }

        [Required]
        public DateTime LastDate { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        public JobPosting()
        {
            Id = 0;
            Email = string.Empty;
            JobTitle = string.Empty;
            Company = string.Empty;
            JobDescription = string.Empty;
            Location = string.Empty;
            Type = string.Empty;
            SalaryRange = string.Empty;
            Experience = string.Empty;
            Qualifications = string.Empty;
            LastDate = DateTime.Now;
            PostedDate = DateTime.Now;
        }
    }
}
