namespace P01_StudentSystem.Data.Models
{
    public class Homework
    {
        public int HomeworkId { get; set; }
        public string Content { get; set; }
        public ContentT ContentType { get; set; }
        public DateTime Submissiontime { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public enum ContentT
        {
            Application,
            Pdf, 
            Zip
        }

    }
}
