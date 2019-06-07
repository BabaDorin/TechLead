using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TechLead.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}