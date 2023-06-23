using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;
namespace P03_SalesDatabase.Data.Seeder
{
    public class ProductSeeder : ISeeder<Product>
    {
        private static readonly List<Product> products = new()
        {
            new Product
            {
                ProductId = 1,
                Name = "Shower gel",
                Price = 13.0M,
                Quantity = 1,
            },

            new Product
            {
                ProductId = 2,
                Name = "Paper",
                Price = 5.0M,
                Quantity = 1,
            },
        };

        public void Seed(EntityTypeBuilder<Product> builder) => builder.HasData(products);
    }
}
