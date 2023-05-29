using MovieManagement.DAL.Entities;
using MovieManagement.DAL.Repositories.Contracts;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;

namespace MovieManagement.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }
       

        public async Task<int> CreateAsync(CategoryDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow._categoryRepository.CreateAsync(new Category
            {
                category_id = entity.category_id,
                name = entity.name,
                description = entity.description,
              
            });
            _uow.Commit();

            return id;
        }

        public async Task<CategoryDTO> GetAsync(int id)
        {
            var entity = await _uow._categoryRepository.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new CategoryDTO
            {
                category_id = entity.category_id,
                name = entity.name,
                description = entity.description,
            };
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var list = await _uow._categoryRepository.GetAllAsync();
            var result = new List<CategoryDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new CategoryDTO
            {
                category_id = entity.category_id,
                name = entity.name,
                description = entity.description,
            }));

            return result;
        }
        public async Task UpdateAsync(CategoryDTO entity)
        {
            // Mapping without AutoMapper
            await _uow._categoryRepository.UpdateAsync(new Category
            {
                category_id = entity.category_id,
                name = entity.name,
                description = entity.description,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(int id)
        {
            await _uow._categoryRepository.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
