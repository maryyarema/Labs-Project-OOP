using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagementEF.UserManagementEF.DAL.Helpers.Contracts;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;
using UserManagementEF.UserManagementEF.DAL.Data;
using UserManagementEF.UserManagementEF.DAL.Paging;

namespace UserManagementEF.UserManagementEF.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly UserManagementContext dbContext;
        protected readonly DbSet<TEntity> entities;
        private readonly IDataShaper<TEntity> _dataShaper;

        protected GenericRepository(UserManagementContext dbContext, IDataShaper<TEntity> dataShaper)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<TEntity>();

            _dataShaper = dataShaper;
        }


        public abstract Task<int> CreateAsync(TEntity entity); // the method must return the id of the added entity
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(BaseParameters? parameters = null)
        {
            return await entities.AsNoTracking().ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => entities.Update(entity));
        }
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) throw new Exception($"{typeof(TEntity).Name} with id: [{id}] was not found");

            await Task.Run(() => entities.Remove(entity));
        }

        public virtual async Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters)
        {
            var collection = entities.AsNoTracking();

            if (parameters == null) return await Task.Run(() =>
                    DurationList<ExpandoObject>.ToDurationList(_dataShaper.ShapeData(collection, "").AsQueryable(), 1, 10));

            return await Task.Run(() =>
                DurationList<ExpandoObject>.ToDurationList(
                    _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                    parameters.FilmNumber,
                    parameters.FilmSize));
        }
        public virtual async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
        {
            var entity = await entities.FindAsync();

            return entity == null ?
                null :
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }

        public virtual async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.Run(() => entities.Where(expression).AsNoTracking());
        }
    }
}
