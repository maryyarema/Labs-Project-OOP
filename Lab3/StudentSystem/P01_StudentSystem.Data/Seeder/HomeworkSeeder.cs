using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Seeder
{
    public class HomeworkSeeder : ISeeder<Homework>
    {
        private static readonly List<Homework> homeworks = new()
        {
            new Homework()
            {
              HomeworkId = 1,
              Content = "C:\\User\\file1.pdf",
              ContentType = Homework.ContentT.Pdf,
              CourseId = 1,
              StudentId = 1,
              Submissiontime = new DateTime(2022, 3, 4)
            },

            new Homework()
            {
              HomeworkId = 2,
              Content = "C:\\User\\file2.zip",
              ContentType = Homework.ContentT.Zip,
              CourseId = 2,
              StudentId = 2,
              Submissiontime = new DateTime(2022, 3, 10)
            },
        };
        public void Seed(EntityTypeBuilder<Homework> builder)
        {
            builder.HasData(homeworks);
        }
    }
}
