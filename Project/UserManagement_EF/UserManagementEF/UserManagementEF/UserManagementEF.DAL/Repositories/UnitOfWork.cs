using Microsoft.AspNetCore.Identity;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;
using UserManagementEF.UserManagementEF.DAL.Data;
using UserManagementEF.UserManagementEF.DAL.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UserManagementContext dbContext { get; set; }
        public IMovieRepository Movies { get; }
        public IRaitingRepository Raitings { get; }
        public ICommentRepository Comments { get; }
        public IUserRepository Users { get; }


        public UserManager<User> _userManager { get; set; }
        public RoleManager<IdentityRole<int>> _roleManager { get; set; }

        public UnitOfWork(
            UserManagementContext dbContext,


        IMovieRepository movies,
        IRaitingRepository raitings,
        ICommentRepository comments,
        IUserRepository users,

      

            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager
            )
        {
            this.dbContext = dbContext;


            Movies = movies;
            Raitings = raitings;
            Comments = comments;
            Users = users;

            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}