using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder
               .ToTable(nameof(Location))
               .HasKey(p => p.Id);
            builder.Property(nameof(Location.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Location.Name)).HasMaxLength(20).IsRequired();
            builder.Property(nameof(Location.CountryRegion)).HasMaxLength(20);
        }
    }
    public class LocationDTOValidator : AbstractValidator<LocationBaseDTO>
    {
        public LocationDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a Location")
                .MaximumLength(20).WithMessage("Location cannot be longer than 20 characters");
            RuleFor(p => p.CountryRegion)
                .MaximumLength(20).WithMessage("Country/Region cannot be longer than 20 characters");
        }
    }
}