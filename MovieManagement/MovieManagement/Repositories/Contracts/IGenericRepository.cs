using MovieManagement.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManagement.Repositories.Contracts
{
    internal interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<int> AddRangeAsync(IEnumerable<T> list);
        Task ReplaceAsync(T t);
        Task<int> AddAsync(T t);
    }

   /* internal interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>> TopFiveMovieAsync();
    }*/
}
