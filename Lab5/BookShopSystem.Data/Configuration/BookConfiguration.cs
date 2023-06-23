using BookShopSystem.Data.Seeding;
using BookShopSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(b => b.BookCategories).WithOne(b => b.Book).HasForeignKey(b => b.BookId).HasPrincipalKey(b => b.BookId);
            builder.Property(b=>b.Title).IsUnicode(true);
            builder.Property(b=>b.Description).IsUnicode(true);

            new BookSeeder().Seed(builder);

        }
    }
}
