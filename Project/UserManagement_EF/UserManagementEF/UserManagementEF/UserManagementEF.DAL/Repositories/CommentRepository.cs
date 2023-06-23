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
  
        public class CommentRepository : GenericRepository<Comment>, ICommentRepository
        {
        private readonly ISortHelper<Comment> _sortHelper;
            private readonly IDataShaper<Comment> _dataShaper;

            public CommentRepository(
                UserManagementContext dbContext,
                ISortHelper<Comment> sortHelper,
                IDataShaper<Comment> dataShaper)
                : base(dbContext, dataShaper)
            {
                _sortHelper = sortHelper;
                _dataShaper = dataShaper;
            }


            public override async Task<int> CreateAsync(Comment comment)
            {
                await entities.AddAsync(comment);

                return comment.CommentId;
            }
            public override async Task<IEnumerable<Comment>> GetAllAsync(BaseParameters? parameters = null)
            {
                if (parameters == null) return await base.GetAllAsync(parameters);
                var collection = entities.AsNoTracking();

                if (parameters is not CommentParameters param)
                    return await collection
                        .OrderBy(entity => entity.CommentId)
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
            public override async Task<Comment?> GetByIdAsync(int id)
            {
                return await entities
                    .Include(c => c.Movie)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(m => m.CommentId == id);
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
                var entity = (await GetByConditionAsync(temp => temp.CommentId.Equals(id)))
                    .FirstOrDefault();

                return entity == null ? null :
                    _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
            }
        }
    }
