using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using MediatR;

namespace Crud.Application.Animes.Commands;

public  class DeleteAnimeCommand : IRequest<Anime>
{
    public int Id { get; set; }

    public class DeleteAnimeCommandHandler : IRequestHandler<DeleteAnimeCommand, Anime>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAnimeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Anime> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
        {
            var deleteAnime = await _unitOfWork.AnimeRepository.DeleteAnime(request.Id);

            if (deleteAnime == null)
                throw new InvalidOperationException("Anime não encontrado");

            await _unitOfWork.CommitAsync();

            return deleteAnime;
        }
    }
}
