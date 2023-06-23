using System.Dynamic;
using UserManagementEF.DAL.Entities;
using UserManagementEF.DAL.Repositories.Contracts;
using UserManagementEF.DAL.Data;
using UserManagementEF.DAL.Paging.Entities;
using Microsoft.EntityFrameworkCore;
using UserManagementEF.DAL.Helpers.Contracts;
using UserManagementEF.DAL.Paging;
using UserManagementEF.DAL.Repositories;
using UserManagementEF.DAL.Paging.Entities;

namespace UserManagementEF.DAL.Repository
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly ISortHelper<Movie> _sortHelper;
        private readonly IDataShaper<Movie> _dataShaper;

        public AuthorRepository(
            SchoolLibraryContext dbContext,
            ISortHelper<Movie> sortHelper,
            IDataShaper<Movie> dataShaper)
            : base(dbContext, dataShaper)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public override async Task<Guid> CreateAsync(Movie movie)
        {
            await entities.AddAsync(movie);

            return author.MovieId;
        }
        public override async Task<IEnumerable<Movie>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync(parameters);
            var collection = entities.AsNoTracking();

            if (parameters is not MovieParameters param) // filtering
                return await collection
                    .OrderBy(a => a.MovieId)
                    .ToListAsync();


            var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

            return await newCollection
                //.OrderBy(a => a.AuthorId) after sorting, it makes no sense to sort by id
            
                .ToListAsync();
        }

        
    }
}
