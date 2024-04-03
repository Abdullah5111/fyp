using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

    public class ResumePdf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int resumeId { get; set; }
        [Required]
        public byte[] Pdf { get; set; }
    }

