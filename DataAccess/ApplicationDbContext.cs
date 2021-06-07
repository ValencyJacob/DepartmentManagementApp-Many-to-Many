using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<DivisionEmployee> DivisionEmployeesModel { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configure
            modelBuilder.Entity<DivisionEmployee>().HasKey(x => new { x.Employee_Id, x.Division_Id }); // Create composite key.
            modelBuilder.Entity<EmployeePosition>().HasKey(x => new { x.Position_Id, x.Employee_Id });
        }
    }
}
