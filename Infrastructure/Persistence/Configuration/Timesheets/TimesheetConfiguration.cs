using Domain.Timesheets;
using Domain.Timesheets.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Timesheets
{
    internal class TimesheetConfiguration : IEntityTypeConfiguration<Timesheet>
    {
        public void Configure(EntityTypeBuilder<Timesheet> builder)
        {
            builder
               .ToTable(nameof(Timesheet))
               .HasKey(p => p.Id);
            builder.HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.EmployeeId);
            builder.HasOne(p => p.ApprovalStatus)
                .WithMany()
                .HasForeignKey(p => p.ApprovalStatusId);
            builder.Property(nameof(Timesheet.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Timesheet.TimesheetNumber)).HasMaxLength(20).IsRequired();
            builder.Property(nameof(Timesheet.PeriodStartDate)).IsRequired();
            builder.Property(nameof(Timesheet.PeriodEndDate)).IsRequired();

            builder.Ignore(nameof(Timesheet.TotalHours));
        }
    }

    public class TimesheetDTOValidator : AbstractValidator<TimesheetBaseDTO>
    {
        public TimesheetDTOValidator()
        {
            RuleFor(p => p.TimesheetNumber)
                .NotEmpty().WithMessage("You must enter a Timesheet Number")
                .MaximumLength(20).WithMessage("Timesheet Number cannot be longer than 20 characters");

            RuleFor(p => p.EmployeeId)
                .NotEmpty().WithMessage("Employee is Required");

            RuleFor(p => p.PeriodStartDate)
                .NotEmpty().WithMessage("Start Date is Required");

            RuleFor(p => p.PeriodEndDate)
                .NotEmpty().WithMessage("End date is required")
                .GreaterThan(m => m.PeriodStartDate).WithMessage("End date must be after Start date");

            RuleFor(p => p.ApprovalStatusId)
                .NotEmpty().WithMessage("Approval Status is Required");
        }
    }
}