using Microsoft.EntityFrameworkCore;

namespace project_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.Customer)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<TimeRegistration> TimeRegistration { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}
