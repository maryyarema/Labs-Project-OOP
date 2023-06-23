using System.Reflection;
using System.Text;
using System.Data;
using Dapper;
using System.ComponentModel;
using System.Data.SqlClient;
using UserManagementEF.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq.Expressions;
using UserManagementEF.DAL.Helpers.Contracts;
using UserManagementEF.DAL.Paging.Entities;

namespace UserManagement.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SchoolLibraryContext dbContext;
        protected readonly DbSet<TEntity> entities;
        private readonly IDataShaper<TEntity> _dataShaper;

        protected GenericRepository(SchoolLibraryContext dbContext, IDataShaper<TEntity> dataShaper)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<TEntity>();

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

        public virtual async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters)
        {
            var collection = entities.AsNoTracking();

            if (parameters == null) return await Task.Run(() =>
                    PagedList<ExpandoObject>.ToPagedList(_dataShaper.ShapeData(collection, "").AsQueryable(), 1, 10));

            return await Task.Run(() =>
                PagedList<ExpandoObject>.ToPagedList(
                    _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                    parameters.PageNumber,
                    parameters.PageSize));
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
