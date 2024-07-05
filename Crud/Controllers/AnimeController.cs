using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnimeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> AddAnime(Anime anime)
        {
            var animeNew = await _unitOfWork.AnimeRepository.AddAnime(anime);

            await _unitOfWork.CommitAsync();
            return Ok(animeNew);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnime(int id)
        {
            var deleteAnime = await _unitOfWork.AnimeRepository.DeleteAnime(id);

            if (deleteAnime == null)
                return NotFound("Anime não encontrado");

            await _unitOfWork.CommitAsync();

            return Ok(deleteAnime);
        }
    }
}
