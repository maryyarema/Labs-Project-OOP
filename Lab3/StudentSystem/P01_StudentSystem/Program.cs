using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;

internal class Program
{
    public static void Main()
    {
        Build();
    }
    private static void Build()
    {
        var courses = ReadTable<Course>();
        var students = ReadTable<Student>();

        Console.WriteLine("Courses: ");

        foreach (var course in courses)
        {
            Console.WriteLine(course);
        }

        Console.WriteLine("Students: ");

        foreach (var student in students)
        {
            Console.WriteLine(student);
        }

    }

    public static List<TEntity> ReadTable<TEntity>() where TEntity : class
    {
        using (var context = new StudentSystemContext())
        {
            var table = context.Set<TEntity>();
            return table.ToList();
        }
    }
}