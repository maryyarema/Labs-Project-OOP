using System.Dynamic;
using UserManagementEF.UserManagementEF.API.Mapping.Configurations;
using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.BLL.Services.Contracts;
using UserManagementEF.UserManagementEF.DAL.Entities;
using UserManagementEF.UserManagementEF.DAL.Paging;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;

namespace UserManagementEF.UserManagementEF.BLL.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _uow;

        public MovieService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<int> CreateAsync(MovieDTO entity)
        {
            // We create a Book object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Movie movie = MappingFunctions.MapSourceToDestination<MovieDTO, Movie>(entity);

            var id = await _uow.Movies.CreateAsync(movie);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<MovieDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Movie, MovieDTO>
                (await _uow.Movies.GetAllAsync(parameters));
        }
        public async Task<MovieDTO?> GetAsync(int id)
        {
            // Get entity from db
            Movie? movie = await _uow.Movies.GetByIdAsync(id);

            // We create a BookDTO object and copy the values ​​of the properties
            // of the movie object into its properties (we perform mapping)
            MovieDTO? movieDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                movie == null ?
                null : MappingFunctions.MapSourceToDestination<Movie, MovieDTO>(movie);

            return movieDTO;
        }
        public async Task UpdateAsync(MovieDTO entity)
        {
            // We create a Book object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
                Movie movie = MappingFunctions.MapSourceToDestination<MovieDTO, Movie>(entity);

            await _uow.Movies.UpdateAsync(movie);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow.Movies.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Movies.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
        {
            return await _uow.Movies.GetById_DataShaping_Async(id, parameters);
        }
    }
}
