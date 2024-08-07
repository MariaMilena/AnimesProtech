﻿using ProtechAnimes.Domain.Entities;
using ProtechAnimes.Domain.Interfaces;
using MediatR;
using System.Xml.Linq;

namespace ProtechAnimes.Application.Animes.Commands;

public class UpdateAnimeCommand : AnimeCommandBase
{
    public int Id { get; set; }

    public class UpdateAnimeCommandHandler : IRequestHandler<UpdateAnimeCommand, Anime>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAnimeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Anime> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
        {
            var existingAnime = await _unitOfWork.AnimeRepository.GetAnimeById(request.Id);

            if (existingAnime is null)
                throw new InvalidOperationException("Anime não encontrado.");

            existingAnime.Name = request.Name;
            existingAnime.Summary = request.Summary;
            existingAnime.Director = request.Director;

            _unitOfWork.AnimeRepository.UpdateAnime(existingAnime);

            await _unitOfWork.CommitAsync();

            return existingAnime;
        }
    }
}
