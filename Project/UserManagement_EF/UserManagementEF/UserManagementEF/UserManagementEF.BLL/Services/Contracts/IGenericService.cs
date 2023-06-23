using System.Dynamic;
using UserManagementEF.UserManagementEF.DAL.Paging;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;

namespace UserManagementEF.UserManagementEF.BLL.Services.Contracts
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<int> CreateAsync(TEntity entity);
        Task<TEntity?> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters parameters);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        
        Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null);
        Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null);
    }
}
