using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using UserManagementEF.DAL.Helper.Contracts;

namespace UserManagementEF.DAL.Helpers
{
    public class SortHelper<T> : ISortHelper<T>
    {
        public IQueryable<T> ApplySort(IQueryable<T> entities, string? orderByString)
        {
            // If there are no books, we exit the method immediately
            // If we’ve got some books, but the query string is empty, we want to order the books by title
            if (!entities.Any() || string.IsNullOrWhiteSpace(orderByString)) return entities;

            
            // splitting our query string to get the individual fields
            var orderParams = orderByString.Trim().Split(',');
            // For checking if the field received through the query string really exists in class
            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var strBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param)) continue;


                var propFromQueryName = param.Trim().Split(" ")[0]; // get parameter name
                var objProperty = propertyInfo.FirstOrDefault(pi => // searching this param in class
                    pi.Name.Equals(propFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objProperty == null) continue; // don't find

                // How should orderby property
                var sortOrder = param.EndsWith("desc") ? "descending" : "ascending";

                strBuilder.Append($"{objProperty.Name} {sortOrder}, ");
            }


            // Removing excess commas
            var orderQuery = strBuilder.ToString().TrimEnd(',', ' ');

            return entities.OrderBy(orderQuery); // using System.Linq.Dynamic.Core (Nuget package)
        }
    }
}
