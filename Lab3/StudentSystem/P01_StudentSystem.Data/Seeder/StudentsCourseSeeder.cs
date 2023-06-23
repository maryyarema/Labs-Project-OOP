using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder.Contract;

namespace P01_StudentSystem.Data.Seeder
{
    public class StudentsCourseSeeder : ISeeder<StudentCourse>
    {
        private static readonly List<StudentCourse> studcourse = new()
        {
            new StudentCourse()
            {
              StudentId = 1,
              CourseId = 1,
            },

            new StudentCourse()
            {
              StudentId = 2,
              CourseId = 2,
            },
        };
        public void Seed(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasData(studcourse);
        }
    }
}
