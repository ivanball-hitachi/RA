using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class Employee_ReviewerConfiguration : IEntityTypeConfiguration<Employee_Reviewer>
    {
        public void Configure(EntityTypeBuilder<Employee_Reviewer> builder)
        {
            builder
               .ToTable(nameof(Employee_Reviewer))
               .HasKey(p => p.Id);
            builder.HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.EmployeeId);
            builder.HasOne(p => p.Reviewer)
                .WithMany()
                .HasForeignKey(p => p.ReviewerId);
            builder.Property(nameof(Employee.Id)).ValueGeneratedOnAdd();
        }
    }

    public class Employee_ReviewerDTOValidator : AbstractValidator<Employee_ReviewerBaseDTO>
    {
        public Employee_ReviewerDTOValidator()
        {
        }
    }
}