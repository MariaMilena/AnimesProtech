using ProtechAnimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProtechAnimes.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Anime> Animes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
}
