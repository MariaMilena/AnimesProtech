﻿using ProtechAnimes.Domain.Entities;
using ProtechAnimes.Domain.Interfaces;
using MediatR;

namespace ProtechAnimes.Application.Animes.Queries;

public class GetAnimeByIdQuery : IRequest<Anime>
{
    public int Id { get; set; }
    public class GetAnimeByIdQueryHandler : IRequestHandler<GetAnimeByIdQuery, Anime>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAnimeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Anime> Handle(GetAnimeByIdQuery request, CancellationToken cancellationToken)
        {
            var anime = await _unitOfWork.AnimeRepository.GetAnimeById(request.Id);

            return anime;
        }
    }
}
