using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sqlite
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json")
                .AddEnvironmentVariables(prefix: "DOTNET_")
                .AddUserSecrets<Program>(optional: true);
            var configuration = builder.Build();

            Console.WriteLine(configuration["Books:ConnectionString"]);

            using(var context=new SchoolContext())
            {
                var newCourse = new Course
                {
                    CourseName = "Math",
                };

                context.Courses.Add(newCourse);
                await context.SaveChangesAsync();

                var newStudent = new Student
                {
                    Name = "John",
                    CourseId = newCourse.CourseId
                };

                context.Students.Add(newStudent);
                await context.SaveChangesAsync();

                var studentsWithSameName =await context.Students
                    .Where(s => s.Name == GetName())
                    .ToListAsync();

                var studentWithCourse =await context.Students
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync();

                Console.WriteLine($"{studentsWithSameName[0].Name} take {studentsWithSameName[0].Course.CourseName} course.");
                Console.WriteLine($"{studentWithCourse.Name} take {studentWithCourse.Course.CourseName} course.");
            }
        }

        private static string GetName()
        {
            return "John";
        }
    }
}
