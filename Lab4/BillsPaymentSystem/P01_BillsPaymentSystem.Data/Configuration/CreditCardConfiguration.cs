using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace P01_BillsPaymentSystem.Data.Configuration
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder
                .HasOne(c => c.PaymentMethod)
                .WithOne(c => c.CreditCard);

            new CreditCardSeeder().Seed(builder);
        }
    }
}
