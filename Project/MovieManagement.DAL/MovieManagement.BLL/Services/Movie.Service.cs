using MovieManagement.DAL.Entities;
using MovieManagement.DAL.Repositories.Contracts;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;

namespace MovieManagement.BLL.Services
{
    public class MovieService : IMovieService
    {
        IUnitOfWork _uow;

        public MovieService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<int> CreateAsync(MovieDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow._movieRepository.CreateAsync(new Movie
            {
                movie_id = entity.movie_id,
                title = entity.title,
                release_year = entity.release_year,
                director = entity.director,
                description = entity.description,

            });
            _uow.Commit();

            return id;
        }

        public async Task<MovieDTO> GetAsync(int id)
        {
            var entity = await _uow._movieRepository.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new MovieDTO
            {
                movie_id = entity.movie_id,
                title = entity.title,
                release_year = entity.release_year,
                director = entity.director,
                description = entity.description,
            };
        }
        public async Task<IEnumerable<MovieDTO>> GetAllAsync()
        {
            var list = await _uow._movieRepository.GetAllAsync();
            var result = new List<MovieDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new MovieDTO
            {
                movie_id = entity.movie_id,
                title = entity.title,
                release_year = entity.release_year,
                director = entity.director,
                description = entity.description,
            }));

            return result;
        }
        public async Task UpdateAsync(MovieDTO entity)
        {
            // Mapping without AutoMapper
            await _uow._movieRepository.UpdateAsync(new Movie
            {
                movie_id = entity.movie_id,
                title = entity.title,
                release_year = entity.release_year,
                director = entity.director,
                description = entity.description,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow._movieRepository.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}



 