using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace P01_StudentSystem.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            new CourseSeeder().Seed(builder);
        }
    }
}
