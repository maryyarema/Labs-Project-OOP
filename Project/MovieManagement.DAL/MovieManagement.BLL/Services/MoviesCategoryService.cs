using MovieManagement.DAL.Entities;
using MovieManagement.DAL.Repositories.Contracts;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;

namespace MovieManagement.BLL.Services
{
    public class MovieCategoryService : IMovieCategoryService
    {
        IUnitOfWork _uow;

        public MovieCategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<int> CreateAsync(MovieCategoryDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow._movieCategoryRepository.CreateAsync(new MovieCategory
            {
                category_id = entity.category_id,
                movie_id = entity.movie_id,

            });
            _uow.Commit();

            return id;
        }

        public async Task<MovieCategoryDTO> GetAsync(int id)
        {
            var entity = await _uow._movieCategoryRepository.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new MovieCategoryDTO
            {
                category_id = entity.category_id,
                movie_id = entity.movie_id,
            };
        }
        public async Task<IEnumerable<MovieCategoryDTO>> GetAllAsync()
        {
            var list = await _uow._movieCategoryRepository.GetAllAsync();
            var result = new List<MovieCategoryDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new MovieCategoryDTO
            {
                category_id = entity.category_id,
                movie_id = entity.movie_id,
            }));

            return result;
        }
        public async Task UpdateAsync(MovieCategoryDTO entity)
        {
            // Mapping without AutoMapper
            await _uow._movieCategoryRepository.UpdateAsync(new MovieCategory
            {
                category_id = entity.category_id,
                movie_id = entity.movie_id,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow._movieCategoryRepository.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
