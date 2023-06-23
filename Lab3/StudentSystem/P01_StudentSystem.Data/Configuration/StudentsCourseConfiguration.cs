using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Configuration
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            new StudentsCourseSeeder().Seed(builder);
        }
    }
}
