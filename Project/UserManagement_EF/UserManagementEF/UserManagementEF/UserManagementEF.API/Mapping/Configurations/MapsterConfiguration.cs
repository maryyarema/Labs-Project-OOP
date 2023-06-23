using Mapster;
using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.DAL.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using UserManagementEF.UserManagementEF.DAL.Entities.Identity;
using System.Xml.Linq;

namespace UserManagementEF.UserManagementEF.API.Mapping.Configurations
{
    public static class MapsterConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            RegisterMovieConfig();
            RegisterCommentConfig();
            RegisterRatingConfig();
            RegisterAddRoleModel();


            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }

        private static void RegisterMovieConfig()
        {
           
            TypeAdapterConfig<MovieDTO, Movie>
                .NewConfig()
               
                .TwoWays();
        }
        
        private static void RegisterCommentConfig()
        {
            TypeAdapterConfig<Comment, CommentDTO>
                .NewConfig()
                .Map(dest => dest.MovieTitle, src => src.Movie.Title)
                .TwoWays();
        }
        private static void RegisterRatingConfig()
        {
            TypeAdapterConfig<Rating, RatingDTO>
                .NewConfig()

                .Map(dest => dest.MovieTitle, src => src.Movie.Title)
                .TwoWays();
        }
            

        private static void RegisterRegisterModelConfig()
        {
            TypeAdapterConfig<RegisterModel, User>
                .NewConfig()
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => 
                    new PasswordHasher<User>(null).HashPassword(null!, src.Password!));
        }
        private static void RegisterAddRoleModel()
        {
            TypeAdapterConfig<RegisterModel, AddRoleModel>
                .NewConfig()
                .TwoWays()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);
        }
    }
}
