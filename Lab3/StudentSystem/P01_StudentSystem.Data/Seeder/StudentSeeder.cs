using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Seeder
{
    public class StudentSeeder : ISeeder<Student>
    {
        private static readonly List<Student> students = new()
        {
            new Student()
            {
                StudentId = 1,
                Birthdaty = new DateTime(2004, 1, 6),
                Name = "Tomas",
                PhoneNumber = "0661702363",
                RegisteredOn = true
            },

            new Student()
            {
                StudentId = 2,
                Birthdaty = new DateTime(2003, 4, 12),
                Name = "John",
                PhoneNumber = "0993485373",
                RegisteredOn = false
            },
        };
        public void Seed(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(students);
        }
    }
}
