using Dapper;

using MovieManagement.Entities;
using MovieManagement.Repositories.Contracts;

using System.Data;
using System.Data.SqlClient;

namespace MovieManagement.Repositories
{
    public class MovieRepository : IGenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Movies")
        {
        }

        public async Task<IEnumerable<Category>> TopFiveCategoryAsync()
        {
            string sql = @"SELECT TOP 5 * FROM Category";
            var results = await _sqlConnection.QueryAsync<Movie>(sql,
                transaction: _dbTransaction);
            return results;
        }
    }
}
