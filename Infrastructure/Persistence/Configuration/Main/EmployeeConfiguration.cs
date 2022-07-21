using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
               .ToTable(nameof(Employee))
               .HasKey(p => p.Id);
            builder.HasOne(p => p.EmployeeType)
                .WithMany()
                .HasForeignKey(p => p.EmployeeTypeId);
            builder.Property(nameof(Employee.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Employee.FirstName)).HasMaxLength(20).IsRequired();
            builder.Property(nameof(Employee.LastName)).HasMaxLength(20).IsRequired();
        }
    }

    public class EmployeeDTOValidator : AbstractValidator<EmployeeBaseDTO>
    {
        public EmployeeDTOValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("You must enter a First Name")
                .MaximumLength(20).WithMessage("First Name cannot be longer than 20 characters");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("You must enter a Last Name")
                .MaximumLength(20).WithMessage("Last Name cannot be longer than 20 characters");
        }
    }
}