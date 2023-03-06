using CompanyEmployees.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Data.Contexts
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() { }

        public EmployeeContext(DbContextOptions options) : base(options) { }

        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);

            modelBuilder.Entity<EmployeeModel>()
                .HasOne(x => x.ChiefEmployee)
                .WithMany(x => x.Subordinates)
                .HasForeignKey(x => x.ChiefEmployeeId);
        }
    }
}
