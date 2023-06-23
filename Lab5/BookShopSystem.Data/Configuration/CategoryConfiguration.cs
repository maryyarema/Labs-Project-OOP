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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.BookCategories).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId).HasPrincipalKey(c => c.CategoryId);
            builder.Property(c => c.Name).IsUnicode(true);

            new CategorySeeder().Seed(builder);
        }
    }
}
