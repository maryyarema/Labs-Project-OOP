using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementEF.DAL.Entities;

namespace UserManagementEF.DAL.Data.Configurations
{
    public class MoviesConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .HasKey(m => m.MovieId);

            builder // one-to-many  Movies - Comments
               .HasMany(m => m.Comments)
               .WithOne(c => c.Movie)
               .HasForeignKey(c => c.MovieId);

            builder // one-to-many  Movies - Raitings
                .HasMany(m => m.Raitings)
                .WithOne(c => c.Movie)
               .HasForeignKey(c => c.MovieId);

        }
    }
}
