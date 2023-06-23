namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ResourceT ResourceType { get; set; }

        public enum ResourceT
        {
            Video, 
            Presentation,
            Document,
            Other
        }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
