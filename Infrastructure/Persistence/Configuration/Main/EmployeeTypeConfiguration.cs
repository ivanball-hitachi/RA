using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class EmployeeTypeConfiguration : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> builder)
        {
            builder
               .ToTable(nameof(EmployeeType))
               .HasKey(p => p.Id);
            builder.Property(nameof(EmployeeType.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(EmployeeType.Name)).HasMaxLength(20).IsRequired();
        }
    }
    public class EmployeeTypeDTOValidator : AbstractValidator<EmployeeTypeBaseDTO>
    {
        public EmployeeTypeDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter an EmployeeType")
                .MaximumLength(20).WithMessage("EmployeeType cannot be longer than 20 characters");
        }
    }
}