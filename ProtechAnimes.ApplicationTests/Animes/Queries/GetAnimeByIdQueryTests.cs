
using ProtechAnimes.Application.Animes.Queries;
using ProtechAnimes.Domain.Entities;
using ProtechAnimes.Domain.Interfaces;
using Moq;
using static ProtechAnimes.Application.Animes.Queries.GetAnimeByIdQuery;

namespace ProtechAnimes.ApplicationTests.Animes.Queries;

public class GetAnimeByIdQueryTests
{
    [Fact]
    public async Task QueryWithValidId_Executed_Success()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        var existingAnime = new Anime("Nome original", "Resumo original", "Diretor original");
        unitOfWork.Setup(u => u.AnimeRepository.GetAnimeById(It.IsAny<int>())).ReturnsAsync(existingAnime);

        var getAnimeByIdQuery = new GetAnimeByIdQuery { Id = 1 };

        var getAnimeByIdQueryHandler = new GetAnimeByIdQueryHandler(unitOfWork.Object);

        // Act
        var result = await getAnimeByIdQueryHandler.Handle(getAnimeByIdQuery, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingAnime.Name, result.Name);
        Assert.Equal(existingAnime.Summary, result.Summary);
        Assert.Equal(existingAnime.Director, result.Director);
    }

    [Fact]
    public async Task QueryWithNonExistentId_Fail()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.AnimeRepository.GetAnimeById(It.IsAny<int>())).ReturnsAsync((Anime)null);

        var getAnimeByIdQuery = new GetAnimeByIdQuery { Id = 1 };

        var getAnimeByIdQueryHandler = new GetAnimeByIdQueryHandler(unitOfWork.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => getAnimeByIdQueryHandler.Handle(getAnimeByIdQuery, new CancellationToken()));
    }
}
