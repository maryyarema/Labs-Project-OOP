using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementEF.DAL.Data.Configurations;
using UserManagementEF.DAL.Entities;

namespace UserManagementEF.DAL.Data
{
    public class UserManagementContext : IdentityDbContext<User, int>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
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
