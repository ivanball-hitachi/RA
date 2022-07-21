using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class LinePropertyConfiguration : IEntityTypeConfiguration<LineProperty>
    {
        public void Configure(EntityTypeBuilder<LineProperty> builder)
        {
            builder
               .ToTable(nameof(LineProperty))
               .HasKey(p => p.Id);
            builder.Property(nameof(LineProperty.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(LineProperty.Name)).HasMaxLength(20).IsRequired();
        }
    }
    public class LinePropertyDTOValidator : AbstractValidator<LinePropertyBaseDTO>
    {
        public LinePropertyDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a LineProperty")
                .MaximumLength(20).WithMessage("LineProperty cannot be longer than 20 characters");
        }
    }
}