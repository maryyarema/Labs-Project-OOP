using Dapper;

using MovieManagement.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;
using MovieManagement.DAL.Entities;

namespace MovieManagement.DAL.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "MovieManagement.Movies")
        {
        }

        public async Task<IEnumerable<Movie>> TopFiveMovieAsync()
        {
            string sql = @"SELECT TOP 5 * FROM Movies";
            var results = await _sqlConnection.QueryAsync<Movie>(sql,
                transaction: _dbTransaction);
            return results;
        }
    }
}
