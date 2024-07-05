using Crud.Application.Animes.Commands;
using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public AnimeController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAnime(int id)
        {
            var anime = await _unitOfWork.AnimeRepository.GetAnimeById(id);

            if (anime == null) 
                NotFound("Anime não encontrado");

            return Ok(anime);
        }

        [HttpPost]
        public async Task<ActionResult> AddAnime(CreateAnimeCommand command)
        {
            var createdANime = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAnime), new { id = createdANime.Id }, createdANime);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnime(int id, UpdateAnimeCommand command)
        { 
            command.Id = id;

            var updatedAnime = await _mediator.Send(command);

            return updatedAnime != null ? Ok(updatedAnime) : NotFound("Anime não encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnime(int id)
        {
            var command = new DeleteAnimeCommand { Id = id };

            var deletedAnime = await _mediator.Send(command);

            return deletedAnime != null ? Ok(deletedAnime) : NotFound("Anime não encontrado");
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimes(
            [FromQuery] string? diretor,
            [FromQuery] string? nome,
            [FromQuery] string? resumo,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        {
            var (animes, totalRecords) = await _unitOfWork.AnimeRepository.GetAnimes(nome, resumo, diretor, pageIndex, pageSize);

            var result = new
            {
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Data = animes
            };

            return Ok(animes);
        }
    }
}
