using Microsoft.EntityFrameworkCore;
namespace Jobverse.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<CompanyInsights> CompanyInsights { get; set; }
        public DbSet<CompanyDepartments> CompanyDepartments { get; set; }
        public DbSet<CompanyReview> CompanyReviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the primary key for the Company class
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Email);

            modelBuilder.Entity<CompanyProfile>()
             .Property(cp => cp.Id)
             .UseIdentityColumn();
            modelBuilder.Entity<CompanyProfile>()
                .HasOne(cp => cp.Company)
                .WithOne(c => c.CompanyProfile)
                .HasForeignKey<CompanyProfile>(cp => cp.CompanyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyInsights>()
                .HasOne(ci => ci.Company)
                .WithOne(c => c.CompanyInsights)
                .HasForeignKey<CompanyInsights>(ci => ci.CompanyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyDepartments>()
               .HasOne(cd => cd.Company)
               .WithOne(c => c.CompanyDepartments)
               .HasForeignKey<CompanyDepartments>(cd => cd.CompanyEmail)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
               .HasMany(c => c.CompanyReviews)
               .WithOne(cr => cr.Company)
               .HasForeignKey(cr => cr.CompanyEmail)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyInsights>();
            modelBuilder.Entity<CompanyProfile>();
            modelBuilder.Entity<CompanyDepartments>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
