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
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _uow;

        public RatingService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<int> CreateAsync(RatingDTO entity)
        {
            // We create a Review object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Rating rating = MappingFunctions.MapSourceToDestination<RatingDTO, Rating>(entity);

            await SeedingReviewObject(entity, rating);

            var id = await _uow.Raitings.CreateAsync(rating);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<RatingDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Rating, RatingDTO>
                (await _uow.Raitings.GetAllAsync(parameters));
        }
        public async Task<RatingDTO?> GetAsync(int id)
        {
            // Get entity from db
            Rating? rating = await _uow.Raitings.GetByIdAsync(id);

            // We create a ReviewDTO object and copy the values ​​of the properties
            // of the rating object into its properties (we perform mapping)
            RatingDTO? ratingDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                rating == null ?
                null : MappingFunctions.MapSourceToDestination<Rating, RatingDTO>(rating);

            return ratingDTO;
        }
        public async Task UpdateAsync(RatingDTO entity)
        {
            // We create a Loan object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Rating rating = MappingFunctions.MapSourceToDestination<RatingDTO, Rating>(entity);

            await SeedingReviewObject(entity, rating);

            await _uow.Raitings.UpdateAsync(rating);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow.Raitings.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
        

        public async Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Raitings.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
        {
            return await _uow.Raitings.GetById_DataShaping_Async(id, parameters);
        }

        private async Task SeedingReviewObject(RatingDTO entity, Rating rating)
        {
            var movie = (await _uow.Movies.GetAllAsync()).ToList()
              .FirstOrDefault(b => b.Title == entity.MovieTitle);


            if (movie == null) throw new Exception("No movie with this title was found");


            rating.Movie = movie;
        }
    }
}
