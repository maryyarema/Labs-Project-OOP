namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public ICollection<StudentCourse> StudentsCources { get; set; }
        public ICollection<Resource> Resources { get; set; }

        public ICollection<Homework> Homeworks { get; set; }

        public override string ToString()
        {
            return $"CourseId: {CourseId}\n Name: {Name}\n Description: {Description}\n StartDate: {StartDate}\n" +
                $"EndDate: {EndDate}\n Price: {Price}\n";
        }
    }
}
