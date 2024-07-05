
using Crud.Domain.Entities;

namespace Crud.Domain.Interfaces;

public interface IAnimeDapperRepository
{
    Task<Anime> GetAnimeById(int id);
    Task<IEnumerable<Anime>> GetAnimes(String? name, String? summary, String? director, int pageIndex, int pageSize);
}
