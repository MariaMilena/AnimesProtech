using ProtechAnimes.Domain.Interfaces;
using ProtechAnimes.Infrastructure.Data;

namespace ProtechAnimes.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IAnimeRepository? _animeRepository;
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IAnimeRepository AnimeRepository
    {
        get
        {
            return _animeRepository = _animeRepository ?? new AnimeRepository(_appDbContext);
        }
    }

    public async Task CommitAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}
