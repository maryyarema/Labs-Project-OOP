using AutoMapper;
using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.BLL.Services.Contracts;
using UserManagementEF.UserManagementEF.DAL.Entities;
using UserManagementEF.UserManagementEF.DAL.Paging;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserManagementEF.UserManagementEF.API.Mapping.Configurations;
using UserManagementEF.UserManagementEF.DAL.Entities.Identity;

namespace UserManagementEF.UserManagementEF.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _uow { get; set; }
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        
        public UserService(IUnitOfWork uow, IMapper mapper, IOptions<JWT> jwt)
        {
            _uow = uow;
            _mapper = mapper;
            _jwt = jwt.Value;
        }


        public async Task<int> CreateAsync(UserDTO entity)
        {
            // We create a User object and copy the values of the properties
            // of the entity object into its properties (we perform mapping)
            User user = _mapper.Map<User>(entity);

            var id = await _uow.Users.CreateAsync(user);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>
                (await _uow.Users.GetAllAsync(parameters));
        }
        public async Task<UserDTO?> GetAsync(int     id)
        {
            // Get entity from db
            User? user = await _uow.Users.GetByIdAsync(id);

            // We create a UserDTO object and copy the values of the properties
            // of the user object into its properties (we perform mapping)
            UserDTO? userDTO = _mapper.Map<UserDTO?>(user);

            return  userDTO;
        }
        public async Task<User?> GetUserWithRefreshTokensAsync(int id)
        {
            return await _uow.Users.GetByIdAsync(id);
        }
        public async Task UpdateAsync(UserDTO entity)
        {
            // We create a User object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            User user = _mapper.Map<User>(entity);

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow.Users.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Users.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
        {
            return await _uow.Users.GetById_DataShaping_Async(id, parameters);
        }

        
        public async Task<string> RegisterAsync(RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password)) throw new Exception("Password is empty!");
            if (string.IsNullOrWhiteSpace(model.Email)) throw new Exception("Email is empty!");
            
            // Mapping using Mapster
            var user = MappingFunctions.MapSourceToDestination<RegisterModel, User>(model);
            // To check if the email is already registered with our api
            var userWithSameEmail = await _uow._userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null) return "Email is already registered!";

            var result = await _uow._userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) await _uow._userManager.AddToRoleAsync(user, Authorization.default_role.ToString());

            return $"User registered with username {user.UserName}";
        }
        public async Task<string> RegisterAdministratorAsync(RegisterModel model)
        {
            // Register new user
            var result = await RegisterAsync(model);
            // Checking whether the user has been created correctly
            if (model.UserName == null || !result.EndsWith(model.UserName)) return result;
            
            // Mapping using Mapster
            var addRoleModel = MappingFunctions.MapSourceToDestination<RegisterModel, AddRoleModel>(model);
            addRoleModel.Role = "Administrator";
            
            // Add to user new role "Administrator"
            result = result + ". " + await AddRoleAsync(addRoleModel);
            
            // Checking whether the role has been added
            if (model.Email == null || !result.EndsWith(model.Email)) return result;
            
            return $"User-Administrator registered with username {model.UserName}";
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _uow._userManager.FindByEmailAsync(model.Email ?? "");
            if (user == null) return "No Accounts Registered with this email!";

            if (!await _uow._userManager.CheckPasswordAsync(user, model.Password ?? ""))
                return "Incorrect Credentials for user!";
            
            //  if the user is a valid one
            var roleExists = Enum
                .GetNames(typeof(Authorization.Roles))
                .Any(x => x.ToLower() == model.Role?.ToLower());

            
            // Check if the passed Role is present in our system. If not, throws an error message
            if (!roleExists) return $"Role {model.Role} not found!";
            
            var validRole = Enum
                .GetValues(typeof(Authorization.Roles))
                .Cast<Authorization.Roles>()
                .FirstOrDefault(x => x.ToString().ToLower() == model.Role?.ToLower());

            await _uow._userManager.AddToRoleAsync(user, validRole.ToString());
            
            // Add the role to the valid user
            return $"Added {model.Role} to user {model.Email}!";
        }

        
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            // Creating a new Response Object,
            var authenticationModel = new AuthenticationModel();
            // Checking if the passeed email is valid
            var user = await _uow._userManager.FindByEmailAsync(model.Email!);

            // Return a message if not valid
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "No Accounts Registered with this email";
                return authenticationModel;
            }

            // Checking if the password is valid
            if (await _uow._userManager.CheckPasswordAsync(user, model.Password ?? string.Empty))
            {
                authenticationModel.IsAuthenticated = true;
                
                // Call the CreateJWTToken function
                JwtSecurityToken token = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;

                var roles = await _uow._userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = roles.ToList();


                // Check if there are any active refresh tokens available for the authenticated user
                if (user.RefreshTokens == null || user.RefreshTokens.Any(a => a.IsActive))
                {
                    // Set the available active refresh token to response
                    var activeRefreshToken = user.RefreshTokens?.FirstOrDefault(a => a.IsActive);
                    if (activeRefreshToken != null)
                    {
                        authenticationModel.RefreshToken = activeRefreshToken.Token;
                        authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                    }
                }
                else
                {
                    // If there are not active Refresh Token available, we call our
                    // CreateRefreshToken method to generate a refresh token
                    var refreshToken = CreateRefreshToken();

                    //  Once generated, we set the details of the Refresh Token to the Response Object
                    authenticationModel.RefreshToken = refreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    
                    // Finally, we need to add these tokens into our RefreshTokens Table, so that we can reuse them
                    await _uow._userManager.UpdateAsync(user);
                    await _uow.SaveChangesAsync();
                }
                
                
                // Return the response object
                return authenticationModel;
            }
            
            // else returns a message saying incorrect credentials
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = "Incorrect Credentials for user";
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            // Gets all the claims of the user(user details)
            var userClaims = await _uow._userManager.GetClaimsAsync(user);
            // Gets all the roles of the user
            var roles = await _uow._userManager.GetRolesAsync(user);

            // Creating a new JWT Security Token and returns them
            var roleClaims = new List<Claim>();
            roles.ToList().ForEach(role => roleClaims.Add(new Claim("roles", role)));

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                   /* new Claim(JwtRegisteredClaimNames.Jti, int.NewGuid().ToString()),*/
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim("uid", user.Id.ToString()),
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key!));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        
        public async Task<AuthenticationModel> GetRefreshTokenAsync(string token)
        {
            // Create a new Response object
            var authenticationModel = new AuthenticationModel();
            
            // Check if there any matching user for the token in database
            var user = _uow._userManager.Users
                .SingleOrDefault(u => u.RefreshTokens == null || u.RefreshTokens.Any(t => t.Token == token));
            if (user == null) // If no matching user found, pass a message “Token did not match any users.”
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Token did not match any users.";
                
                return authenticationModel;
            }

            //  Get the Refresh token object of the matching record
            var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);
            
            // Check is the selected token is active, if not active, send a message “Token Not Active.”
            if (refreshToken == null || !refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Token Not Active.";
                
                return authenticationModel;
            }

            // Revoke Current Refresh Token. Every time we request a new JWT, we have to make sure
            // that we replace the refresh token with a new one
            refreshToken.Revoked = DateTime.UtcNow;
            
            // Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens?.Add(newRefreshToken);
            await _uow._userManager.UpdateAsync(user);
            await _uow.SaveChangesAsync();
            
            // Generates new jwt
            authenticationModel.IsAuthenticated = true;
            
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            
            var roleList = await _uow._userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            
            return authenticationModel;
        }
        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = _uow._userManager.Users
                .SingleOrDefault(u => u.RefreshTokens != null && u.RefreshTokens.Any(t => t.Token == token));

            // Return false if no user found with token
            if (user == null) return await Task.FromResult(false);

            var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);
            
            // Return false if token is not active
            if (refreshToken == null || refreshToken.IsActive) return await Task.FromResult(false);
            
            // If the passed refresh token is valid, we revoke it here and save to the database
            refreshToken.Revoked = DateTime.UtcNow;
            await _uow._userManager.UpdateAsync(user);
            await _uow.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
