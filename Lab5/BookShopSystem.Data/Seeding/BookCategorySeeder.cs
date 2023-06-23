using BookShopSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Data.Seeding
{
    public class BookCategorySeeder : ISeeder<BookCategory>
    {
        List<BookCategory> bookCategories = new()
        {
            new BookCategory
            {
                BookId = 1,
                CategoryId = 2,
            },
            new BookCategory
            {
                BookId = 2,
                CategoryId = 4,
            },
            new BookCategory
            {
                BookId = 3,
                CategoryId = 1,
            },
            new BookCategory
            {
                BookId = 4,
                CategoryId = 3,
            },
            new BookCategory
            {
                BookId = 5,
                CategoryId = 2
            },
        };
        public void Seed(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasData(bookCategories);
        }
    }
}
