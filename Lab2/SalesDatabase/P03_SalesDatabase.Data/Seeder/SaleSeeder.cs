using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.Seeder
{
    public class SaleSeeder : ISeeder<Sale>
    {
        private static readonly List<Sale> sales = new()
        {
            new Sale
            {
                SaleId = 1,
                CustomerId = 1,
                StoreId = 1,
                ProductId = 1,
                Date = new DateTime(2011, 6, 10)

            },

          new Sale
            {
                SaleId = 2,
                CustomerId = 2,
                StoreId = 2,
                ProductId = 2,
                Date = new DateTime(2021, 3, 11)
            },
        };

        public void Seed(EntityTypeBuilder<Sale> builder) => builder.HasData(sales);
    }
}
