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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(a => a.Books).WithOne(a => a.Author).HasForeignKey(a=> a.AuthorId).HasPrincipalKey(a=>a.AuthorId);
            builder.Property(a => a.FirstName).IsUnicode(true).IsRequired(false);
            builder.Property(a => a.LastName).IsUnicode(true);

            new AuthorSeeder().Seed(builder);

        }
    }
}
