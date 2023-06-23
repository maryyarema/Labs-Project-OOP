using System.Dynamic;
using UserManagementEF.DAL.Paging.Entities;
using System.Linq.Expressions;
using UserManagementEF.DAL.Paging;

namespace UserManagementEF.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        Task<int> CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

        Task<FilmList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
