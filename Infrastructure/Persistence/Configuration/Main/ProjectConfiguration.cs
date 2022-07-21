using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
               .ToTable(nameof(Project))
               .HasKey(p => p.Id);
            builder.Property(nameof(Project.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Project.Code)).HasMaxLength(20).IsRequired();
            builder.Property(nameof(Project.Name)).HasMaxLength(20).IsRequired();
        }
    }
    public class ProjectDTOValidator : AbstractValidator<ProjectBaseDTO>
    {
        public ProjectDTOValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("You must enter a Project Code")
                .MaximumLength(20).WithMessage("Project Code cannot be longer than 20 characters");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a Project Name")
                .MaximumLength(20).WithMessage("Project Name cannot be longer than 20 characters");
        }
    }
}