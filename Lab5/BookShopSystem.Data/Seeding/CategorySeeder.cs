using BookShopSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Data.Seeding
{
    public class CategorySeeder : ISeeder<Category>
    {
        List<Category> categories = new List<Category>()
        {
            new Category
            {
                CategoryId = 1,
                Name = "Horror"
            },
            new Category
            {
                CategoryId = 2, 
                Name = "Drama"
            },
            new Category
            {
                CategoryId = 3,
                Name = "Mystery"
            },
            new Category
            {
                CategoryId= 4,
                Name = "Humor"
            }
        };
        public void Seed(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(categories);
        }
    }
}
