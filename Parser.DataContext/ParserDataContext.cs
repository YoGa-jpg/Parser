using Microsoft.EntityFrameworkCore;
using Parser.DataContext.Entities;
using System.Reflection;

namespace Parser.DataContext
{
    public class ParserDataContext : DbContext
    {
        public ParserDataContext(DbContextOptions<ParserDataContext> options) : base(options) { }
        public ParserDataContext() : base() { }

        public DbSet<AverageMark> AverageMarks { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SportSection> Sections { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
