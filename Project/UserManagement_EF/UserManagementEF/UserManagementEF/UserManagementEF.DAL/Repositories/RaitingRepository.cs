using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using UserManagementEF.UserManagementEF.DAL.Data;
using UserManagementEF.UserManagementEF.DAL.Entities;
using UserManagementEF.UserManagementEF.DAL.Helpers.Contracts;
using UserManagementEF.UserManagementEF.DAL.Paging;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;

namespace UserManagementEF.UserManagementEF.DAL.Repositories
{

    public class RatingRepository : GenericRepository<Rating>, IRaitingRepository
    {
        private readonly ISortHelper<Rating> _sortHelper;
        private readonly IDataShaper<Rating> _dataShaper;

        public RatingRepository(
            UserManagementContext dbContext,
            ISortHelper<Rating> sortHelper,
            IDataShaper<Rating> dataShaper)
            : base(dbContext, dataShaper)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public override async Task<int> CreateAsync(Rating raiting)
        {
            await entities.AddAsync(raiting);

            return raiting.RatingId;
        }
        public override async Task<IEnumerable<Rating>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync(parameters);
            var collection = entities.AsNoTracking();

            if (parameters is not CommentParameters param)
                return await collection
                    .OrderBy(entity => entity.RatingId)
                    .Skip((parameters.FilmNumber - 1) * parameters.FilmSize)
                    .Take(parameters.FilmSize)
                    .Include(entity => entity.Movie)
                    .Include(entity => entity.User)
                    .ToListAsync();


            var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await newCollection
                //.OrderBy(entity => entity.LoanId) after sorting, it makes no sense to sort by id
                .Skip((parameters.FilmNumber - 1) * parameters.FilmSize)
                .Take(parameters.FilmSize)
                .Include(entity => entity.Movie)
                .Include(entity => entity.User)
                .ToListAsync();
        }
        public override async Task<Rating?> GetByIdAsync(int id)
        {
            return await entities
                .Include(r => r.Movie)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RatingId == id);
        }

        public override async Task<DurationList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters)
        {
            if (parameters == null) return await base.GetAll_DataShaping_Async(parameters);
            var collection = entities.AsNoTracking(); // filtering

            if (parameters is not UserParameters param)
                return await Task.Run(() =>
                    DurationList<ExpandoObject>.ToDurationList(
                        _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                        parameters.FilmNumber,
                        parameters.FilmSize));


            collection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await Task.Run(() =>
                DurationList<ExpandoObject>.ToDurationList(
                    _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                    parameters.FilmNumber,
                    parameters.FilmSize));
        }
        public override async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
        {
            var entity = (await GetByConditionAsync(temp => temp.RatingId.Equals(id)))
                .FirstOrDefault();

            return entity == null ? null :
                _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
        }
    }
}
