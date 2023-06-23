using P03_SalesDatabase.Data.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure (EntityTypeBuilder<Sale> builder)
        {
            new SaleSeeder().Seed(builder);
        }
    }
}
