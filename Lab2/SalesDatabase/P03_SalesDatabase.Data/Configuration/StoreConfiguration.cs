using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Seeder;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.Configuration
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            new StoreSeeder().Seed(builder);
        }
    }
}
