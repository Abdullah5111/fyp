using ApplyForJob.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplyForJob.DbContexts
{
    public class JobApplicationContext : DbContext
    {
        public JobApplicationContext(DbContextOptions<JobApplicationContext> options) : base(options)
        {
        }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
