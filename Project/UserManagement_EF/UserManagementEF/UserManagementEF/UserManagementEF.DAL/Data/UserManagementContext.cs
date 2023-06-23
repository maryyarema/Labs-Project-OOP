using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementEF.UserManagementEF.DAL.Data.Configurations;
using UserManagementEF.UserManagementEF.DAL.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Data
{
    public class UserManagementContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        /* public DbSet<User> Users { get; set; }*/


        public UserManagementContext(
            DbContextOptions<UserManagementContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CommentsConfiguration());
            modelBuilder.ApplyConfiguration(new MoviesConfiguration());
            modelBuilder.ApplyConfiguration(new RaitingsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());

        }
    }
}
