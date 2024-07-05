using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using MediatR;

namespace Crud.Application.Animes.Commands;

public class CreateAnimeCommand : IRequest<Anime>
{
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Director { get; set; }

    public class CreateAnimeCommandHandler : IRequestHandler<CreateAnimeCommand, Anime>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAnimeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Anime> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
        {
            var newAnime = new Anime(request.Name, request.Summary, request.Director);

            await _unitOfWork.AnimeRepository.AddAnime(newAnime);

            await _unitOfWork.CommitAsync();

            return newAnime;
        }
    }
}
