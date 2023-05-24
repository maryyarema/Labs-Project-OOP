using MovieManagement.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManagement.Repositories.Contracts
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>> TopFiveMovieAsync();
    }
}