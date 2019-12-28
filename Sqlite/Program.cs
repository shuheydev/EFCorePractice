using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sqlite
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new SchoolContext())
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

                var studentsWithSameName = await context.Students
                    .Where(s => s.Name == "John")
                    .ToListAsync();

                var studentWithCourse = await context.Students
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync();
            }

        }
    }
}
