using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementEF.UserManagementEF.DAL.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Data.Configurations
{
    public class RaitingsConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder
              .HasKey(r => r.RatingId);

            builder
                .Property(r => r.RatingId)
                .HasColumnType("decimal(2, 1)");


            builder // one-to-many  Ratings - Users
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            builder // one-to-many  Ratings - Movies
                .HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId);



        }
    }
}
