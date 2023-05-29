
namespace MovieManagement.BLL.Services.Consracts
{
    public interface IGenericService<TEntity>
    {
        Task<int> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

    }
}
