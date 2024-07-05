using Crud.Domain.Entities;

namespace Crud.Domain.Interfaces;

public interface IAnimeRepository
{
    Task<Anime> AddAnime(Anime anime);
    Task<Anime> GetAnimeById(int id);
    Task<Anime> DeleteAnime(int id);
    void UpdateAnime(Anime anime);
}
