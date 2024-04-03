using Microsoft.EntityFrameworkCore;
using jobPosting.Models;

namespace jobPosting.DbContexts
{
    public class JobPostingContext : DbContext
    {
        public JobPostingContext(DbContextOptions<JobPostingContext> options) : base(options)
        {
        }

        public DbSet<JobPosting> JobPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
