using System.Dynamic;
using System.Linq.Expressions;
using UserManagementEF.UserManagementEF.DAL.Paging;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;

namespace UserManagementEF.UserManagementEF.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<int> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null);
        Task<TEntity?> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

        Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null);

        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
