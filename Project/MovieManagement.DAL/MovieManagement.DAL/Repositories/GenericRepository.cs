using System.Reflection;
using System.Text;
using System.Data;
using Dapper;
using System.ComponentModel;
using System.Data.SqlClient;
using MovieManagement.DAL.Repositories.Contracts;

namespace MovieManagement.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected SqlConnection sqlConnection;
        protected IDbTransaction dbTransaction;
        protected readonly string tableName;

        // For creating Update/Insert query
        private static IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();



        public GenericRepository(
             SqlConnection sqlConnection,
             IDbTransaction dbTransaction,
             string tableName)
        {
            this.sqlConnection = sqlConnection;
            this.dbTransaction = dbTransaction;
            this.tableName = tableName;
        }


        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            var insertQuery = GenerateInsertQuery();

            return await sqlConnection.ExecuteScalarAsync<int>(
                insertQuery,
                param: entity,
                transaction: dbTransaction);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await sqlConnection.QueryAsync<TEntity>(
                $"SELECT * FROM {tableName}",
                transaction: dbTransaction);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            // string nameId = $"{typeof(TEntity).Name + "Id"}";
            string nameId = $"{char.ToLower(typeof(TEntity).Name[0]) + typeof(TEntity).Name.Substring(1) + "_id"}";

            var retult = await sqlConnection.QuerySingleOrDefaultAsync<TEntity>(
                $"SELECT * FROM {tableName} WHERE {nameId}=@Id",
                param: new { Id = id },
                transaction: dbTransaction);

            if (retult == null) throw new Exception($"Error in: public virtual async Task<{typeof(TEntity).Name}> GetAsync(int id)");
            else return retult;
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            var updateQuery = GenerateUpdateQuery();

            await sqlConnection.ExecuteAsync(
                updateQuery,
                param: entity,
                transaction: dbTransaction);
        }
        public async Task DeleteAsync(int id)
        {
            //string nameId = $"{typeof(TEntity).Name + "Id"}";
            string nameId = $"{char.ToLower(typeof(TEntity).Name[0]) + typeof(TEntity).Name.Substring(1) + "_id"}";


            await sqlConnection.ExecuteAsync(
                $"DELETE FROM {tableName} WHERE {nameId}=@Id",
                param: new { Id = id },
                transaction: dbTransaction);
        }


        // For update/insert query
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

            insertQuery.Append('(');

            var properties = GenerateListOfProperties(GetProperties);
            string nameId = $"{char.ToLower(typeof(TEntity).Name[0]) + typeof(TEntity).Name.Substring(1) + "_id"}";
            properties.Remove(nameId);

            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append("); SELECT SCOPE_IDENTITY()");

            Console.WriteLine(insertQuery.ToString());

            return insertQuery.ToString();
        }
        private string GenerateUpdateQuery()
        {
            // PK tables Users => UserId
            // string nameId = $"{typeof(TEntity).Name + "Id"}";
            string nameId = $"{char.ToLower(typeof(TEntity).Name[0]) + typeof(TEntity).Name.Substring(1) + "_id"}";


            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals($"{nameId}"))
                    updateQuery.Append($"{property}=@{property},");
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append($" WHERE {nameId}=@{nameId}");

            return updateQuery.ToString();
        }
    }
}
