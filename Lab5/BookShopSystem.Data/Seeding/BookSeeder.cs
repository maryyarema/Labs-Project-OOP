using BookShopSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Data.Seeding
{
    public class BookSeeder : ISeeder<Book>
    {
        List<Book> books = new List<Book>()
        {
            new Book
            {
                BookId = 1,
                AuthorId = 4,
                Title = "A Fanatic Heart",
                Description = "A fantastic book about drama in society",
                Copies = 5000,
                Price = 1500.00M,
                EditionType = Book.EdType.Normal,
                AgeRestriction = Book.ARType.Minor,
                ReleaseDate = new DateTime(2021, 3, 1),
            },
            new Book
            {
                BookId = 2,
                AuthorId = 3,
                Title = "A Handful of Dust",
                Description = "A book about humor",
                Copies = 3040,
                Price = 654.30M,
                EditionType = Book.EdType.Promo,
                AgeRestriction = Book.ARType.Minor,
                ReleaseDate = new DateTime(2020, 2, 12),
            },
            new Book
            {
                BookId = 3,
                AuthorId = 1,
                Title = "A Passage to India",
                Description = "A book about horror",
                Copies = 7500,
                Price = 1230.00M,
                EditionType = Book.EdType.Gold,
                AgeRestriction = Book.ARType.Teen,
                ReleaseDate = new DateTime(2019, 3, 23),
            },
            new Book
            {
                BookId = 4,
                AuthorId = 2,
                Title = "A Scanner Darkly",
                Description = "A book about mystery",
                Copies = 1500,
                Price = 130.00M,
                EditionType = Book.EdType.Normal,
                AgeRestriction = Book.ARType.Adult,
                ReleaseDate = new DateTime(2022, 4, 21),
            },
            new Book
            {
                BookId = 5,
                AuthorId = 5,
                Title = "Look Homeward",
                Description = "A book about drama",
                Copies = 150,
                Price = 132.00M,
                EditionType = Book.EdType.Normal,
                AgeRestriction = Book.ARType.Teen,
                ReleaseDate = new DateTime(2009, 11, 1),
            },
        };
        public void Seed(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(books);
        }
    }
}
