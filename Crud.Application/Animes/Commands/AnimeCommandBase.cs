
using Crud.Domain.Entities;
using MediatR;

namespace Crud.Application.Animes.Commands;

public abstract class AnimeCommandBase : IRequest<Anime>
{
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Director { get; set; }
}
