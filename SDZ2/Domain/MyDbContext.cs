using Microsoft.EntityFrameworkCore;
using SDZ2.Domain.Entities;

namespace SDZ2.Domain
{
    public class MyDbContext : DbContext
    {
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public MyDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=MyConsole.db";
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
