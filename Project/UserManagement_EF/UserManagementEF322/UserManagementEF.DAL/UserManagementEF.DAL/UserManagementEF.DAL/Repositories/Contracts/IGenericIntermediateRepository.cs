using UserManagementEF.DAL.Paging.Entities;
using System.Linq.Expressions;

namespace UserManagementEF.DAL.Repository.Contracts
{
    // For Intermediate tables
    public interface IGenericIntermediateRepository<TEntity> where TEntity : class
    {
        Task<(int, int)> CreateAsync(int firstId, int secondId, TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null);
        Task<TEntity?> GetByIdAsync(int firstId, int secondId);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
