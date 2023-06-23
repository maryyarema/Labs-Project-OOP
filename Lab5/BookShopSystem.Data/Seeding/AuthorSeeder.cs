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
    public class AuthorSeeder : ISeeder<Author>
    {
        List<Author> authors = new List<Author>()
        {
            new Author
            {
                AuthorId = 1,
                FirstName = "Tomas",
                LastName = "Shelby",
            },
            new Author
            {
                AuthorId = 2,
                FirstName = "John",
                LastName = "Straud",
            },
            new Author
            {
                AuthorId = 3,
                FirstName = "Tim",
                LastName = "Jackson",
            },
            new Author
            {
                AuthorId = 4,
                FirstName = "Andry",
                LastName = "Pocket",
            },
            new Author
            {
                AuthorId = 5,
                FirstName = "Antoni",
                LastName = "Backet",
            },
        };
        public void Seed(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(authors);
        }
    }
}
