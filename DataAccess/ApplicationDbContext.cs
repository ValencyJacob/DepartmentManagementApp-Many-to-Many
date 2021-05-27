using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<DivisionEmployee> DivisionEmployeesModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configure
            modelBuilder.Entity<DivisionEmployee>().HasKey(x => new { x.Employee_Id, x.Division_Id }); // Create composite key.
        }
    }
}
