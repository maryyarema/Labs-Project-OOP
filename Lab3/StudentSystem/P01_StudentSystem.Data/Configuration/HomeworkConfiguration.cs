using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace P01_StudentSystem.Data.Configuration
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            new HomeworkSeeder().Seed(builder);
        }
    }
}
