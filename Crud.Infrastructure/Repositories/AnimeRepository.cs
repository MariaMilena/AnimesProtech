using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Crud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Crud.Infrastructure.Repositories;

public class AnimeRepository : IAnimeRepository
{
    protected readonly AppDbContext _appDbContext;

    public AnimeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Anime> AddAnime(Anime anime)
    {
        if (anime is null)
            throw new ArgumentNullException(nameof(anime));
        
        await _appDbContext.Animes.AddAsync(anime);

        return anime;
    }

    public async Task<Anime> GetAnimeById(int id)
    {
        var anime = await _appDbContext.Animes.FindAsync(id);

        if (anime is null)
            throw new InvalidOperationException("Anime não encontrado");

        return anime;
    }

    public async Task<Anime> DeleteAnime(int id)
    {
        var anime = await GetAnimeById(id);

        if (anime is null)
            throw new InvalidOperationException("Anime não encontrado");

        _appDbContext.Animes.Remove(anime);

        return anime;
    }

    public void UpdateAnime(Anime anime)
    {
        if (anime is null)
            throw new InvalidOperationException("Anime não encontrado");

        _appDbContext.Animes.Update(anime);

    }

    public async Task<(IEnumerable<Anime>, int)> GetAnimes(
        String? name, 
        String? summary, 
        String? director,
        int pageSize,
        int pageIndex)
    {
        var query = _appDbContext.Animes.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(a => a.Name.ToLower().Contains(name.ToLower()));
        }

        if (!string.IsNullOrEmpty(summary))
        {
            query = query.Where(a => a.Summary.ToLower().Contains(summary.ToLower()));
        }

        if (!string.IsNullOrEmpty(director))
        {
            query = query.Where(a => a.Director.ToLower().Contains(director.ToLower()));
        }

        var totalRecords = await query.CountAsync();

        var animes = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

        return (animes, totalRecords);
    }
}
