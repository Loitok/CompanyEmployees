using CompanyEmployees.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployees.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.HasKey(x => x.EmployeeId);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Surname)
                .IsRequired();

            builder.Property(x => x.SalaryAmount)
                .IsRequired();

            builder.Property(x => x.Position)
                .IsRequired();
        }
    }
}
