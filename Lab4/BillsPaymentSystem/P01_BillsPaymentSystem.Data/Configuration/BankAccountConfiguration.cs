using P01_BillsPaymentSystem.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;


namespace P01_BillsPaymentSystem.Data.Configuration
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder
                .HasOne(b => b.PaymentMethod)
                .WithOne(b => b.BankAccount);

            builder
                .Property(b => b.BankName)
                .IsUnicode(true);

            builder
                .Property(b => b.SWIFTCode)
                .IsUnicode(false);

            new BankAccountSeeder().Seed(builder);
        }

        
    }
}
