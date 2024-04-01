using Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Contexts
{
    public class AuthDbContext:IdentityDbContext
    {
       public AuthDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
