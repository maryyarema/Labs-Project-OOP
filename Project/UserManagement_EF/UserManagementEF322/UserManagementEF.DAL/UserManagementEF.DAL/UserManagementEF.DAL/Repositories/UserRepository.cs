using Dapper;

using MovieManagement.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;
using MovieManagement.DAL.Entities;

namespace MovieManagement.DAL.Repositories
{
    public class UserRepository : GenericRepository<MovieActor>, IUserRepository
    {
        public UserRepository(
           SqlConnection sqlConnection,
           IDbTransaction dbTransaction)
           : base(sqlConnection, dbTransaction, "MovieManagement.MovieActor")
        {
        }
    }
}

