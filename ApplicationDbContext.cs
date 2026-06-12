using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EFCore.Models;
namespace EFCore
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(@"Host=localhost;Port=5432;Database=EfLearnDb1;Username=postgres;Password=281106")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)// паттерн строитель = тоже самое что и свойства в классах + [атрибуты]
        {
            modelBuilder.Entity<Course>().ToTable("Courses");// имя табл
            modelBuilder.Entity<Course>().HasKey(c => c.Id);//PK
            //тут можно делать то же само что я сделал в классах Models
            modelBuilder.Entity<Course>().Property(c=>c.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();

            // настройка отношений => у сущности Course есть 1 Author в это же время у Author есть множество курсов
            // Это отношение обязательно => IsRequired
            // Настройка между 2 сущностями достаточно прописать к 1 из них
            modelBuilder.Entity<Course>().HasOne(c => c.Author).WithMany(c => c.Courses).IsRequired();
            
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired();

        }
    }
}