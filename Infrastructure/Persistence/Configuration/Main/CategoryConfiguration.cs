using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
               .ToTable(nameof(Category))
               .HasKey(p => p.Id);
            builder.Property(nameof(Category.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Category.Name)).HasMaxLength(20).IsRequired();
        }
    }
    public class CategoryDTOValidator : AbstractValidator<CategoryBaseDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a Category")
                .MaximumLength(20).WithMessage("Category cannot be longer than 20 characters");
        }
    }
}