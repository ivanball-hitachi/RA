using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class LegalEntityConfiguration : IEntityTypeConfiguration<LegalEntity>
    {
        public void Configure(EntityTypeBuilder<LegalEntity> builder)
        {
            builder
               .ToTable(nameof(LegalEntity))
               .HasKey(p => p.Id);
            builder.Property(nameof(LegalEntity.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(LegalEntity.Code)).HasMaxLength(4).IsRequired();
            builder.Property(nameof(LegalEntity.Name)).HasMaxLength(20).IsRequired();
        }
    }

    public class LegalEntityDTOValidator : AbstractValidator<LegalEntityBaseDTO>
    {
        public LegalEntityDTOValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("You must enter a Legal Entity Code")
                .MaximumLength(4).WithMessage("Legal Entity Code cannot be longer than 4 characters");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a Legal Entity Name")
                .MaximumLength(20).WithMessage("Legal Entity Name cannot be longer than 20 characters");
        }
    }
}