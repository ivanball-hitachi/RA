using Domain.Timesheets;
using Domain.Timesheets.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Timesheets
{
    internal class TimesheetLineConfiguration : IEntityTypeConfiguration<TimesheetLine>
    {
        public void Configure(EntityTypeBuilder<TimesheetLine> builder)
        {
            builder
               .ToTable(nameof(TimesheetLine))
               .HasKey(p => p.Id);
            builder.HasOne(p => p.LegalEntity)
                .WithMany()
                .HasForeignKey(p => p.LegalEntityId);
            builder.HasOne(p => p.Location)
                .WithMany()
                .HasForeignKey(p => p.LocationId);
            builder.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId);
            builder.HasOne(p => p.Project)
                .WithMany()
                .HasForeignKey(p => p.ProjectId);
            builder.HasOne(p => p.Activity)
                .WithMany()
                .HasForeignKey(p => p.ActivityId);
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.LineProperty)
                .WithMany()
                .HasForeignKey(p => p.LinePropertyId);
            builder.HasOne(p => p.ApprovalStatus)
                .WithMany()
                .HasForeignKey(p => p.ApprovalStatusId);
            builder.Property(nameof(TimesheetLine.Id)).ValueGeneratedOnAdd();
        }
    }

    public class TimesheetLineDTOValidator : AbstractValidator<TimesheetLineBaseDTO>
    {
        public TimesheetLineDTOValidator()
        {
            RuleFor(p => p.LegalEntityId)
                .NotEmpty().WithMessage("Legal Entity is Required");

            RuleFor(p => p.LocationId)
                .NotEmpty().WithMessage("Location is Required");

            RuleFor(p => p.CustomerId)
                .NotEmpty().WithMessage("Customer is Required");

            RuleFor(p => p.ProjectId)
                .NotEmpty().WithMessage("Project is Required");

            RuleFor(p => p.ActivityId)
                .NotEmpty().WithMessage("Activity is Required");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Category is Required");

            RuleFor(p => p.LinePropertyId)
                .NotEmpty().WithMessage("Line Property is Required");

            RuleFor(p => p.ApprovalStatusId)
                .NotEmpty().WithMessage("Approval Status is Required");

            RuleForEach(x => x.TimesheetLineDetails).SetValidator(new TimesheetLineDetailDTOValidator());
        }
    }
}