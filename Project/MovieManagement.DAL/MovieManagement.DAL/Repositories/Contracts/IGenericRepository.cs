namespace MovieManagement.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        Task<int> CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

       
    }
}
