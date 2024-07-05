using Crud.Domain.Entities;

namespace Crud.Domain.Interfaces;

public interface IAnimeRepository
{
    Task<Anime> AddAnime(Anime anime);
    Task<Anime> GetAnimeById(int id);
    Task<Anime> DeleteAnime(int id);
    void UpdateAnime(Anime anime);
    Task<(IEnumerable<Anime> Animes, int TotalRecords)> GetAnimes(String? name, String? summary, String? director, int pageIndex, int pageSize);
}
