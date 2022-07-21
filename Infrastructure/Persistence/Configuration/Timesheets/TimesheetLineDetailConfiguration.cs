using Domain.Timesheets;
using Domain.Timesheets.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Timesheets
{
    internal class TimesheetLineDetailConfiguration : IEntityTypeConfiguration<TimesheetLineDetail>
    {
        public void Configure(EntityTypeBuilder<TimesheetLineDetail> builder)
        {
            builder
               .ToTable(nameof(TimesheetLineDetail))
               .HasKey(p => p.Id);
            builder.Property(nameof(TimesheetLineDetail.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(TimesheetLineDetail.Day)).IsRequired();
            builder.Property(nameof(TimesheetLineDetail.Hours)).IsRequired();
        }
    }

    public class TimesheetLineDetailDTOValidator : AbstractValidator<TimesheetLineDetailBaseDTO>
    {
        public TimesheetLineDetailDTOValidator()
        {
            RuleFor(m => m.Day)
                .NotEmpty().WithMessage("Day is Required");

            RuleFor(p => p.Hours)
                .GreaterThanOrEqualTo(0).WithMessage("Number of Hours should be between 0 and 24")
                .LessThan(24).WithMessage("Number of Hours should be between 0 and 24");
        }
    }
}