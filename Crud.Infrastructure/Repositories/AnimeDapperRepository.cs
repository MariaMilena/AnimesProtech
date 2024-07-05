using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Dapper;
using System.Data;

namespace Crud.Infrastructure.Repositories;

public class AnimeDapperRepository : IAnimeDapperRepository
{
    private readonly IDbConnection _connection;

    public AnimeDapperRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Anime> GetAnimeById(int id)
    {
        string query = "select * from Animes where Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<Anime>(query, new { Id = id });
    }

    public async Task<IEnumerable<Anime>> GetAnimes(string? name, string? summary, string? director, int pageIndex, int pageSize)
    {
        string query = "select * from Animes";
        return await _connection.QueryAsync<Anime>(query);
    }
}
