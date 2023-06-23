using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementEF.DAL.Entities;

namespace UserManagementEF.DAL.Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.UserId);




           
            builder // one-to-many  Users - Raitings
                .HasMany(u => u.Raitings)
                 .WithOne(r => r.User)
                .HasPrincipalKey(r => r.UserId);
           builder // one-to-many  Users - Comments
               .HasMany(u => u.Comments)
               .WithOne(c => c.User)
                .HasPrincipalKey(c => c.UserId);

        }
    }
}
