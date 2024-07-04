using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Anime> Animes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=animeprotech.db",
                b => b.MigrationsAssembly("Crud"));
        }

    }
}
