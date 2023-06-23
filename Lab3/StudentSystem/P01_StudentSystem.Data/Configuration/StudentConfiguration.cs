using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            new StudentSeeder().Seed(builder);
        }
    }
}
