using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.Seeder
{
    public class StoreSeeder : ISeeder<Store>
    {
        private static readonly List<Store> stores = new()
        {
            new Store
            {
                StoreId = 1,
                Name = "Dima's Shop"
            },

          new Store
            {
                StoreId = 2,
                Name = "Rostilav's Shop"
            },
        };

        public void Seed(EntityTypeBuilder<Store> builder) => builder.HasData(stores);
    }
}

