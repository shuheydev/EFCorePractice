
using Microsoft.EntityFrameworkCore;
using Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sqlite
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=School.db");
        }
    }
}
