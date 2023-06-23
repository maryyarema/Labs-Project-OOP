using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Seeder
{
    public class CourseSeeder : ISeeder<Course>
    {
        private static readonly List<Course> courses = new()
        {
            new Course()
            {
              CourseId = 1,
              Description = "HTML, CSS, JS - FRONT",
              StartDate = new DateTime (2022, 3, 4),
              EndDate = new DateTime (2023, 5, 10),
              Name = "FrontEnd",
              Price = 255.0M
            },

            new Course()
            {
              CourseId = 2,
              Description = ".NET Basic",
              StartDate = new DateTime (2023, 3, 4),
              EndDate = new DateTime (2024, 5, 10),
              Name = "BackEnd",
              Price = 300.55M
            },
        };
        public void Seed(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(courses);
        }
    }
}
