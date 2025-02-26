using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infrastructure.DataAccess;

public class TechLibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; } // 'Users'= table name
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {// db file path
        optionsBuilder.UseSqlite("Data Source=");
    }
}
