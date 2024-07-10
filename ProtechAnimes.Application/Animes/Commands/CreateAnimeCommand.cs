using ProtechAnimes.Domain.Entities;
using ProtechAnimes.Domain.Interfaces;
using MediatR;

namespace ProtechAnimes.Application.Animes.Commands;

public class CreateAnimeCommand : AnimeCommandBase
{
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
