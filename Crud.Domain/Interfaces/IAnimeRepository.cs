using Crud.Domain.Entities;

namespace Crud.Domain.Interfaces;

public interface IAnimeRepository
{
    Task<Anime> AddAnime(Anime anime);
}
