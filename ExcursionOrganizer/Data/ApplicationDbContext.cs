using ExcursionOrganizer.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExcursionOrganizer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.Migrate(); // Автоматично приложение на всички миграции при стартиране на приложението
        }
        public DbSet<Excursion> Excursions { get; set; } // Таблица за екскурзии
        public DbSet<User> Users { get; set; } // Таблица за потребители
        public DbSet<Image> Images { get; set; } // Таблица за снимки
        public DbSet<ExcursionUser> ExcursionUsers { get; set; } // Таблица за връзката между екскурзии и потребители (Междинна таблица)
    }
}