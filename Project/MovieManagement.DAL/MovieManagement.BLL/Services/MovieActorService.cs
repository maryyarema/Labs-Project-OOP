using MovieManagement.DAL.Entities;
using MovieManagement.DAL.Repositories.Contracts;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;

namespace MovieManagement.BLL.Services
{
    public class MovieActorService : IMovieActorService
    {
        IUnitOfWork _uow;

        public MovieActorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(MovieActorDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow._movieActorRepository.CreateAsync(new MovieActor
            {
                actor_id = entity.actor_id,
                movie_id = entity.movie_id,
                
            });
            _uow.Commit();

            return id;
        }

        public async Task<MovieActorDTO> GetAsync(int id)
        {
            var entity = await _uow._movieActorRepository.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new MovieActorDTO
            {
                actor_id = entity.actor_id,
                movie_id = entity.movie_id,
            };
        }
        public async Task<IEnumerable<MovieActorDTO>> GetAllAsync()
        {
            var list = await _uow._movieActorRepository.GetAllAsync();
            var result = new List<MovieActorDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new MovieActorDTO
            {
                actor_id = entity.actor_id,
                movie_id = entity.movie_id,
            }));

            return result;
        }
        public async Task UpdateAsync(MovieActorDTO entity)
        {
            // Mapping without AutoMapper
            await _uow._movieActorRepository.UpdateAsync(new MovieActor
            {
                actor_id = entity.actor_id,
                movie_id = entity.movie_id,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow._movieActorRepository.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
