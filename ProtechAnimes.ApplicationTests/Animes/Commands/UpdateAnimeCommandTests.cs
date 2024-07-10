
using ProtechAnimes.Application.Animes.Commands;
using ProtechAnimes.Domain.Entities;
using ProtechAnimes.Domain.Interfaces;
using Moq;
using static ProtechAnimes.Application.Animes.Commands.UpdateAnimeCommand;

namespace ProtechAnimes.ApplicationTests.Animes.Commands;

public class UpdateAnimeCommandTests
{
    [Fact]
    public async Task UpdateAnime_UpdateSuccess()
    {
        var unitOfWork = new Mock<IUnitOfWork>();

        var existingAnime = new Anime("Anime", "Resumo", "Diretor");
        unitOfWork.Setup(ani => ani.AnimeRepository.GetAnimeById(It.IsAny<int>())).ReturnsAsync(existingAnime);

        var updateAnimeCommand = new UpdateAnimeCommand
        {
            Id = existingAnime.Id,
            Name = "Updated Anime",
            Summary = "Updated Summary",
            Director = "Updated Director"
        };

        var updateAnimeCommandHandler = new UpdateAnimeCommandHandler(unitOfWork.Object);

        var updatedAnime = await updateAnimeCommandHandler.Handle(updateAnimeCommand, new CancellationToken());

        Assert.Equal("Updated Anime", updatedAnime.Name);
        Assert.Equal("Updated Summary", updatedAnime.Summary);
        Assert.Equal("Updated Director", updatedAnime.Director);
    }

    [Fact]
    public async Task UpdateCommandWithEmptyName_Fail()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        var updateAnimeCommand = new UpdateAnimeCommand
        {
            Id = 1,
            Name = "", // Nome inválido
            Summary = "Resumo atualizado",
            Director = "Diretor atualizado"
        };

        var updateAnimeCommandHandler = new UpdateAnimeCommandHandler(unitOfWork.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => updateAnimeCommandHandler.Handle(updateAnimeCommand, new CancellationToken()));
    }

    [Fact]
    public async Task UpdateCommandWithNullName_Fail()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        var updateAnimeCommand = new UpdateAnimeCommand
        {
            Id = 1,
            Name = null, // Nome inválido
            Summary = "Resumo atualizado",
            Director = "Diretor atualizado"
        };

        var updateAnimeCommandHandler = new UpdateAnimeCommandHandler(unitOfWork.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => updateAnimeCommandHandler.Handle(updateAnimeCommand, new CancellationToken()));
    }

    [Fact]
    public async Task UpdateCommandWithNonExistentId_Fail()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.AnimeRepository.GetAnimeById(It.IsAny<int>())).ReturnsAsync((Anime)null);

        var updateAnimeCommand = new UpdateAnimeCommand
        {
            Id = 1,
            Name = "Nome válido",
            Summary = "Resumo atualizado",
            Director = "Diretor atualizado"
        };

        var updateAnimeCommandHandler = new UpdateAnimeCommandHandler(unitOfWork.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => updateAnimeCommandHandler.Handle(updateAnimeCommand, new CancellationToken()));
    }
}
