using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementEF.UserManagementEF.DAL.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);





            builder // one-to-many  Users - Raitings
                .HasMany(u => u.Ratings)
                 .WithOne(r => r.User)
                .HasPrincipalKey(r => r.Id);
            builder // one-to-many  Users - Comments
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                 .HasPrincipalKey(c => c.Id);

        }
    }
}
