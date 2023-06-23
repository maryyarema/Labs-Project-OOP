using Microsoft.AspNetCore.Identity;
using UserManagementEF.UserManagementEF.DAL.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IRaitingRepository Raitings { get; }
        ICommentRepository Comments { get; }
        IUserRepository Users { get; }


        UserManager<User> _userManager { get; set; }
        RoleManager<IdentityRole<int>> _roleManager { get; set; }


        Task SaveChangesAsync();
    }
}
