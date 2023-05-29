using MovieManagement.DAL.Entities;
using MovieManagement.DAL.Repositories.Contracts;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;

namespace MovieManagement.BLL.Services
{
    public class ActorService : IActorService
    {
        IUnitOfWork _uow;

        public ActorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(ActorDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow._actorRepository.CreateAsync(new Actor
            {
               // actor_id = entity.actor_id,
                name = entity.name,
                birthdate = entity.birthdate,
                nationality = entity.nationality,
            });
            _uow.Commit();

            return id;
        }

            public async Task<ActorDTO> GetAsync(int id)
        {
            var entity = await _uow._actorRepository.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new ActorDTO
            {
                actor_id = entity.actor_id,
                name = entity.name,
                birthdate = entity.birthdate,
                nationality = entity.nationality,
            };
        }
        public async Task<IEnumerable<ActorDTO>> GetAllAsync()
        {
            var list = await _uow._actorRepository.GetAllAsync();
            var result = new List<ActorDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new ActorDTO
            {
                actor_id = entity.actor_id,
                name = entity.name,
                birthdate = entity.birthdate,
                nationality = entity.nationality,
            }));

            return result;
        }
        public async Task UpdateAsync(ActorDTO entity)
        {
            // Mapping without AutoMapper
            await _uow._actorRepository.UpdateAsync(new Actor
            {
                actor_id = entity.actor_id,
                name = entity.name,
                birthdate = entity.birthdate,
                nationality = entity.nationality,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow._actorRepository.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
