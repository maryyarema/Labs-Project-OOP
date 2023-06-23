using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Configuration
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            new ResourceSeeder().Seed(builder);
        }
    }
}
