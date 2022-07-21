using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder
               .ToTable(nameof(Activity))
               .HasKey(p => p.Id);
            builder.Property(nameof(Activity.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Activity.Number)).HasMaxLength(10).IsRequired();
            builder.Property(nameof(Activity.Name)).HasMaxLength(20).IsRequired();
        }
    }
    public class ActivityDTOValidator : AbstractValidator<ActivityBaseDTO>
    {
        public ActivityDTOValidator()
        {
            RuleFor(p => p.Number)
                .NotEmpty().WithMessage("You must enter an Activity Number")
                .MaximumLength(10).WithMessage("Activity Number cannot be longer than 10 characters");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter an Activity Name")
                .MaximumLength(20).WithMessage("Activity Name cannot be longer than 20 characters");
        }
    }
}