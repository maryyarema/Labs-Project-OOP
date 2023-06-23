using System.Dynamic;
using System.Reflection;
using UserManagementEF.DAL.Helpers.Contracts;

namespace SchoolLibrary_EF.DAL.Helpers
{
    public class DataShaper<T> : IDataShaper<T>
    {
        private PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            // Get all the properties of an input class
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }


        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            // Parsing the input string and returns just the properties we need to return to the controller
            var requiredProperties = GetRequiredProperties(fieldsString);

            // and return extracted values from required prepared properties
            return FetchData(entities, requiredProperties);
        }
        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            // Parsing the input string and returns just the properties we need to return to the controller
            var requiredProperties = GetRequiredProperties(fieldsString);

            // and return extracted values from required prepared properties
            return FetchDataForEntity(entity, requiredProperties);
        }

        // Parses the input string and returns just the properties we need to return to the controller
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            // If the fieldsString is not empty
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                // it split
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                // and check if the fields match the properties in our entity
                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi =>
                        pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    // If they do, add them to the list of required properties
                    if (property != null) requiredProperties.Add(property);
                }
            }
            // if fieldsString is empty, consider all properties to be required
            else requiredProperties = Properties.ToList();

            return requiredProperties;
        }

        // Private methods to extract values from required prepared properties
        private static ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();

            // Iterating over the required properties
            foreach (var property in requiredProperties)
            {
                // and using a bit of reflection, to extract the values and add them to ExpandoObject
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }

            return shapedObject;
        }
        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();

            // Looping through the list of entities
            entities.ToList().ForEach(entity =>
                shapedData.Add(FetchDataForEntity(entity, requiredProperties)));

            // and returning a collection of shaped entities as a result
            return shapedData;
        }
    }
}