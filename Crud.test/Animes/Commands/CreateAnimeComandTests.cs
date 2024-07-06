using Crud.Application.Animes.Commands;
using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Moq;
using static Crud.Application.Animes.Commands.CreateAnimeCommand;

namespace Crud.UnitTests.Animes.Commands;

public class CreateAnimeComandTests
{
    [Fact]
    public async Task CommandIsValid_Executed_Success()
    {
        var unitOfWork = new Mock<IUnitOfWork>();

        var createAnimeCommand = new CreateAnimeCommand
        {
            Name = "Test Anime",
            Summary = "Test Summary",
            Director = "Test Director"
        };

        var newAnime = new Anime(createAnimeCommand.Name, createAnimeCommand.Summary, createAnimeCommand.Director);

        unitOfWork.Setup(ani => ani.AnimeRepository.AddAnime(It.IsAny<Anime>())).Returns(Task.FromResult(newAnime));

        var createAnimeCommandHandler = new CreateAnimeCommandHandler(unitOfWork.Object);

        var AnimeResult = await createAnimeCommandHandler.Handle(createAnimeCommand, new CancellationToken());

        unitOfWork.Verify(ani => ani.AnimeRepository.AddAnime(It.IsAny<Anime>()), Times.Once);

        Assert.NotNull(AnimeResult);

        Assert.Equal(newAnime.Name, AnimeResult.Name);
        Assert.Equal(newAnime.Summary, AnimeResult.Summary);
        Assert.Equal(newAnime.Director, AnimeResult.Director);
    }

    [Fact]
    public async Task CreateCommandWithEmptyName_Fail()
    {
        var unitOfWork = new Mock<IUnitOfWork>();

        var createAnimeCommand = new CreateAnimeCommand
        {
            Name = "", 
            Summary = "Resumo",
            Director = "Diretor"
        };

        var createAnimeCommandHandler = new CreateAnimeCommandHandler(unitOfWork.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => createAnimeCommandHandler.Handle(createAnimeCommand, new CancellationToken()));
    }

    [Fact]
    public async Task CreateCommandWithNullDirector_Fail()
    {
        var unitOfWork = new Mock<IUnitOfWork>();

        var createAnimeCommand = new CreateAnimeCommand
        {
            Name = "Anime",
            Summary = "Resumo",
            Director = null
        };

        var createAnimeCommandHandler = new CreateAnimeCommandHandler(unitOfWork.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => createAnimeCommandHandler.Handle(createAnimeCommand, new CancellationToken()));
    }
}