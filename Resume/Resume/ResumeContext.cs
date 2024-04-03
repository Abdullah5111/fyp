using Microsoft.EntityFrameworkCore;

namespace Resume
{
    public class ResumeContext: DbContext
    {
        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options)
        {
        }

        public DbSet<ResumePdf> resumes { get; set; }
    }
}
