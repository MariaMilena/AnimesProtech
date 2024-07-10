
using ProtechAnimes.Domain.Entities;
using ProtechAnimes.Domain.Interfaces;
using MediatR;

namespace ProtechAnimes.Application.Animes.Queries;

public class GetAnimesQuery : IRequest<(IEnumerable<Anime>, int)>
{
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Director { get; set; }
    public int pageIndex { get; set; }
    public int pageSize { get; set; }

    public class GetAnimesQueryHandler : IRequestHandler<GetAnimesQuery, (IEnumerable<Anime>, int)>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAnimesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<Anime>, int)> Handle(GetAnimesQuery request, CancellationToken cancellationToken)
        {
            var (animes, totalRecords) = await _unitOfWork.AnimeRepository.GetAnimes(request.Name, request.Summary, request.Director, request.pageSize, request.pageIndex);

            return (animes, totalRecords);
        }
    }
}
