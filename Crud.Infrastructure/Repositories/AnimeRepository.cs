using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Crud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
}
