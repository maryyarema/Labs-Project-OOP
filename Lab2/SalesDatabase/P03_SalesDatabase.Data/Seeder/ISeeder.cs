using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_SalesDatabase.Data.Seeder
{
    public interface ISeeder<T> where T : class
    {
        void Seed(EntityTypeBuilder<T> builder);
    }
}
