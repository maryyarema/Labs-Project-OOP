using Microsoft.EntityFrameworkCore;
using UserManagementEF.DAL.Data;
using UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.DAL.Repository.Contracts;
using System.Linq.Expressions;

namespace UserManagementEF.DAL.Repository
{
    // For Intermediate tables
    public abstract class GenericIntermediateRepository<TEntity> : 
        IGenericIntermediateRepository<TEntity> where TEntity : class
    {
        protected readonly SchoolLibraryContext dbContext;
        protected readonly DbSet<TEntity> entities;

        public GenericIntermediateRepository(SchoolLibraryContext dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();
        }


        public virtual async Task<(int, int)> CreateAsync(int firstId, int secondId, TEntity entity)
        {
            await entities.AddAsync(entity);
            return (firstId, secondId);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null)
        {
            return await entities.AsNoTracking().ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(int firstId, int secondId)
        {
            return await entities.FindAsync(firstId, secondId);
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => entities.Update(entity));
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => entities.Remove(entity));
        }

        public virtual async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
