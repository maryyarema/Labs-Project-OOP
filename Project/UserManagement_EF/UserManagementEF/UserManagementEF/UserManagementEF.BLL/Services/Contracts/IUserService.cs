using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.DAL.Entities;
using UserManagementEF.UserManagementEF.DAL.Entities.Identity;

namespace UserManagementEF.UserManagementEF.BLL.Services.Contracts
{
    public interface IUserService : IGenericService<UserDTO>
    {
        Task<User?> GetUserWithRefreshTokensAsync(int id);
        
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> RegisterAdministratorAsync(RegisterModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> GetRefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
