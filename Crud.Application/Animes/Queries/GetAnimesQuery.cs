
using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using MediatR;

namespace Crud.Application.Animes.Queries;

public class GetAnimesQuery : IRequest<IEnumerable<Anime>>
{
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Director { get; set; }
    public int pageIndex { get; set; }
    public int pageSize { get; set; }
    public class GetAnimesQueryHandler : IRequestHandler<GetAnimesQuery, IEnumerable<Anime>>
    {
        private readonly IAnimeDapperRepository _animeDapperRepository;

        public GetAnimesQueryHandler(IAnimeDapperRepository animeDapperRepository)
        {
            _animeDapperRepository = animeDapperRepository;
        }

        public async Task<IEnumerable<Anime>> Handle(GetAnimesQuery request, CancellationToken cancellationToken)
        {
            var animes = await _animeDapperRepository.GetAnimes(request.Name, request.Summary, request.Director, request.pageSize, request.pageIndex);

            return animes;
        }
    }
}
