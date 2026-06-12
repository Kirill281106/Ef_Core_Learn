using EFCore;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;
using Microsoft.Extensions.Logging;

class Program
{
    static void Main()
    {
        Create_New_DB();
        Add_Data();
        Read_Data();
        Update_Data();
    }

    static void Create_New_DB()
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.Migrate(); // создаёт бд и применяет все миграции
    }
    static void Add_Data()
    {
        using var db = new ApplicationDbContext();
        var author1 = new Author
        {
            FirstName = "Стив",
            LastName = "Макконнелл",
            Age = 60
        };
        var courses = new List<Course>
        {
            new Course
            {
                Name = "Совершенный код",
                LessonQuantity = 35,
                CreatedAt = DateTime.UtcNow,
                Price = 1500,
                Author = author1
            },
            new Course
            {
                Name = "Code Complete",
                LessonQuantity = 40,
                CreatedAt = DateTime.UtcNow,
                Price = 2000,
                Author = author1
            }
        };
        db.Authors.Add(author1);//когда 1 автор
        db.Courses.AddRange(courses);// когда надо добавить несколько обектов
        
        db.SaveChanges();//сохраняем данные
        Console.WriteLine("Данные добавлены");
    }
    static void Read_Data()
    {
        Console.WriteLine("___________________________________________________________________________________");
        using var db = new ApplicationDbContext();
        List<Course> course = db.Courses.Include(c => c.Author).ToList();// Получить все курсы с авторами (жадная загрузка)
        foreach (var c  in course)
        {
            Console.WriteLine($"{c.Name}, Автор: {c.Author?.FirstName} {c.Author?.LastName}");
        }
        var expensiveCourses = db.Courses
            .Where(c => c.Price > 1500)//выборка
            .ToList();
        foreach (var c in expensiveCourses)
        {
            Console.WriteLine(c.Name, c.Author);
        }
        var sortedCourses = db.Courses
            .OrderByDescending(c => c.Price)//сортировка
            .ToList();
        foreach (var c in sortedCourses)
        {
            Console.WriteLine($"{c.Name} | {c.Author}");
        }
    }
    static void Update_Data()
    {
        using var db = new ApplicationDbContext();
        Course? courses = db.Courses.FirstOrDefault(c=>c.Name== "Совершенный код");
        courses?.LessonQuantity = 22;
        db.SaveChanges();
        Console.WriteLine("________________________________________");
        Console.WriteLine($"{courses.Name} | {courses.LessonQuantity}");
    }
}