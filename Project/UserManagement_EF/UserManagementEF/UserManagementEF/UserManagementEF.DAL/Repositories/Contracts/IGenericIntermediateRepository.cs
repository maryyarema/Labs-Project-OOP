using System.Linq.Expressions;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Repositories.Contracts
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
