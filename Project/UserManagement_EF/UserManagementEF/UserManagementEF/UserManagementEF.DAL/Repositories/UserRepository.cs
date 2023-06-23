using System.Dynamic;
using UserManagementEF.UserManagementEF.DAL.Entities;
using UserManagementEF.UserManagementEF.DAL.Helpers.Contracts;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;
using UserManagementEF.UserManagementEF.DAL.Repositories.Contracts;
using UserManagementEF.UserManagementEF.DAL.Repositories;
using UserManagementEF.UserManagementEF.DAL.Data;
using Microsoft.EntityFrameworkCore;
using UserManagementEF.UserManagementEF.DAL.Paging;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ISortHelper<User> _sortHelper;
    private readonly IDataShaper<User> _dataShaper;

    public UserRepository(
        UserManagementContext dbContext,
        ISortHelper<User> sortHelper,
        IDataShaper<User> dataShaper)
        : base(dbContext, dataShaper)
    {
        _sortHelper = sortHelper;
        _dataShaper = dataShaper;
    }


    public override async Task<int> CreateAsync(User user)
    {
        await entities.AddAsync(user);

        return user.Id;
    }
    public override async Task<IEnumerable<User>> GetAllAsync(BaseParameters? parameters = null)
    {
        if (parameters == null) return await base.GetAllAsync(parameters);
        var collection = entities.AsNoTracking(); // filtering

        if (parameters is not UserParameters param)
            return await collection
                .OrderBy(a => a.Id)
                .Skip((parameters.FilmNumber - 1) * parameters.FilmSize)
                .Take(parameters.FilmSize)
                .ToListAsync();


        SearchByUserName(ref collection, param.UserName); // searching(after filtering)
        var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

        return await newCollection
            //.OrderBy(a => a.UserId) after sorting, it makes no sense to sort by id
            .Skip((parameters.FilmNumber - 1) * parameters.FilmSize)
            .Take(parameters.FilmSize)
            .ToListAsync();

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


        SearchByUserName(ref collection, param.UserName); // searching(after filtering)
        collection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

        return await Task.Run(() =>
                DurationList<ExpandoObject>.ToDurationList(
                    _dataShaper.ShapeData(collection, parameters.Fields ?? "").AsQueryable(),
                    parameters.FilmNumber,
                    parameters.FilmSize));
    }
    public override async Task<ExpandoObject?> GetById_DataShaping_Async(int id, BaseParameters? parameters = null)
    {
        var entity = (await GetByConditionAsync(temp => temp.Id.Equals(id)))
            .FirstOrDefault();

        return entity == null ? null :
            _dataShaper.ShapeData(entity, parameters?.Fields ?? "");
    }

    private static void SearchByUserName(ref IQueryable<User> entities, string? userName)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(userName)) return;

        entities = entities
            .Where(p => (p.UserName).ToLower().Contains(userName.Trim().ToLower()));
    }
}
