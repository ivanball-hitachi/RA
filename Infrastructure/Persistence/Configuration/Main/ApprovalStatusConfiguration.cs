using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class ApprovalStatusConfiguration : IEntityTypeConfiguration<ApprovalStatus>
    {
        public void Configure(EntityTypeBuilder<ApprovalStatus> builder)
        {
            builder
               .ToTable(nameof(ApprovalStatus))
               .HasKey(p => p.Id);
            builder.Property(nameof(ApprovalStatus.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(ApprovalStatus.Name)).HasMaxLength(20).IsRequired();
        }
    }

    public class ApprovalStatusDTOValidator : AbstractValidator<ApprovalStatusBaseDTO>
    {
        public ApprovalStatusDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a Approval Status")
                .MaximumLength(20).WithMessage("Approval Status cannot be longer than 20 characters");
        }
    }
}