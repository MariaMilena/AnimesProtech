
using Crud.Application.Animes.Commands;
using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Moq;
using static Crud.Application.Animes.Commands.DeleteAnimeCommand;

namespace Crud.UnitTests.Animes.Commands;

public class DeleteAnimeCommandTests
{
    [Fact]
    public async Task DeleteCommandWithNonExistentId_Fail()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.AnimeRepository.GetAnimeById(It.IsAny<int>())).ReturnsAsync((Anime)null);

        var deleteAnimeCommand = new DeleteAnimeCommand { Id = 1 };

        var deleteAnimeCommandHandler = new DeleteAnimeCommandHandler(unitOfWork.Object);

        // Act
        Func<Task> act = async () => await deleteAnimeCommandHandler.Handle(deleteAnimeCommand, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(act);
    }
}
