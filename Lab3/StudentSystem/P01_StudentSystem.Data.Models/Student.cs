namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool RegisteredOn { get; set; }
        public DateTimeOffset? Birthdaty { get; set; }

        public ICollection<StudentCourse> StudentsCources { get; set; }
        public ICollection<Homework> Homeworks { get; set; }

        public override string ToString()
        {
            return $"StudentId: {StudentId}\n Name: {Name}\n PhoneNumber: {PhoneNumber}\n RegisteredOn: {RegisteredOn}\n" +
                $"Birthday: {Birthdaty}\n";
        }
    }
}
