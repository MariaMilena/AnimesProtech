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
}
