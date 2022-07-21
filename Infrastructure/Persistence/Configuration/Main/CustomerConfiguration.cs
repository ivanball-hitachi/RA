using Domain.Main;
using Domain.Main.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Main
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
               .ToTable(nameof(Customer))
               .HasKey(p => p.Id);
            builder.Property(nameof(Customer.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Customer.Name)).HasMaxLength(20).IsRequired();
            builder.Property(nameof(Customer.CustomerAccount)).HasMaxLength(20).IsRequired();
            builder.Property(nameof(Customer.AccountNumber)).HasMaxLength(20);
        }
    }
    public class CustomerDTOValidator : AbstractValidator<CustomerBaseDTO>
    {
        public CustomerDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("You must enter a Customer")
                .MaximumLength(20).WithMessage("Customer cannot be longer than 20 characters");
            RuleFor(p => p.CustomerAccount)
                .NotEmpty().WithMessage("You must enter a Customer Account")
                .MaximumLength(20).WithMessage("Customer Account cannot be longer than 20 characters");
            RuleFor(p => p.AccountNumber)
                .MaximumLength(20).WithMessage("Account number cannot be longer than 20 characters");
        }
    }
}