using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.Configuration
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.PaymentMethodId);

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(p => p.UserId);

            builder
                .HasIndex(pm => new { pm.UserId, pm.BankAccountId, pm.CreditCardId })
                .IsUnique();

            builder
               .HasCheckConstraint("CK_PaymentMethods_BankAccountOrCreditCard",
                   @"((BankAccountId IS NULL AND CreditCardId IS NOT NULL) OR (BankAccountId IS NOT NULL AND CreditCardId IS NULL))");

            new PaymentMethodSeeder().Seed(builder);
        }
    }
}

