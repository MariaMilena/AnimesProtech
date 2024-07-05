
using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using MediatR;

namespace Crud.Application.Animes.Queries;

public class GetAnimeByIdQuery : IRequest<Anime>
{
    public int Id { get; set; }
    public class GetAnimeByIdQueryHandler : IRequestHandler<GetAnimeByIdQuery, Anime>
    {
        private readonly IAnimeDapperRepository _animeDapperRepository;

        public GetAnimeByIdQueryHandler(IAnimeDapperRepository animeDapperRepository)
        {
            _animeDapperRepository = animeDapperRepository;
        }

        public async Task<Anime> Handle(GetAnimeByIdQuery request, CancellationToken cancellationToken)
        {
            var anime = await _animeDapperRepository.GetAnimeById(request.Id);

            return anime;
        }
    }
}
