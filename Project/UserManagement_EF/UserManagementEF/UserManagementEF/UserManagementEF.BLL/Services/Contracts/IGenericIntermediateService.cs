using UserManagementEF.UserManagementEF.DAL.Paging.Entities;

namespace UserManagementEF.UserManagementEF.BLL.Services.Contracts
{
    // For Intermediate tables
    public interface IGenericIntermediateService<TEntity> where TEntity : class
    {
        Task<(int, int )?> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters parameters);
        Task<TEntity?> GetByIdAsync(int firstId, int secondId);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int firstId, int secondId);

        Task<(int, int)> GetIdsToOjbect(TEntity entity);
    }
}
