using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data.Seeder.Contract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Seeder
{
    public class ResourceSeeder : ISeeder<Resource>
    {
        private static readonly List<Resource> resources = new()
        {
            new Resource()
            {
              ResourceId = 1,
              CourseId = 1,
              Name = "SiteCourse",
              ResourceType = Resource.ResourceT.Other,
              Url = "site_course.com"
            },

            new Resource()
            {
              ResourceId = 2,
              CourseId = 2,
              Name = "SiteCourse2",
              ResourceType = Resource.ResourceT.Other,
              Url = "site_course2.com"
            },
        };
        public void Seed(EntityTypeBuilder<Resource> builder)
        {
            builder.HasData(resources);
        }
    }
}
