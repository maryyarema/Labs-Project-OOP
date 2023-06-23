using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace P01_BillsPaymentSystem.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.FirstName)
                .IsUnicode(true);

            builder
                .Property(u => u.LastName)
                .IsUnicode(true);

            builder
                .Property(u => u.Email)
                .IsUnicode(false);

            builder
                .Property(u => u.Password)
                .IsUnicode(false);

            new UserSeeder().Seed(builder);
        }
    }
}
