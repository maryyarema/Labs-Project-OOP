
using UserManagementEF.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;
using UserManagementEF.DAL.Entities;

namespace UserManagementEF.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {

        public CommentRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "UserManagement.Comments")
        {
        }
    }

}
