using Crud.Application.Animes.Queries;
using Crud.Domain.Entities;
using Crud.Domain.Interfaces;
using Moq;

namespace Crud.UnitTests.Animes.Queries;

public class GetAnimesQueryTests
{
    [Fact]
    public async Task QueryIsValid_ReturnsAnimesAndTotalRecords()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var animes = new List<Anime>
            {
                new Anime ( "Anime1", "Summary1", "Director1" ),
                new Anime ( "Anime2", "Summary2",  "Director2" )
            };

        mockUnitOfWork.Setup(repo => repo.AnimeRepository.GetAnimes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync((animes, animes.Count));

        var query = new GetAnimesQuery { Name = "Anime", Summary = "Summary", Director = "Director", pageIndex = 0, pageSize = 10 };
        var handler = new GetAnimesQuery.GetAnimesQueryHandler(mockUnitOfWork.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);  // Verifica se result não é nulo
        Assert.Equal(animes.Count, result.Item2);  // Verifica o número total de registros

        // Verifica se os nomes dos animes retornados estão corretos
        Assert.Equal(animes[0].Name, result.Item1.ElementAt(0).Name);
        Assert.Equal(animes[1].Name, result.Item1.ElementAt(1).Name);

        // Verifica se os resumos dos animes retornados estão corretos
        Assert.Equal(animes[0].Summary, result.Item1.ElementAt(0).Summary);
        Assert.Equal(animes[1].Summary, result.Item1.ElementAt(1).Summary);

        // Verifica se os diretores dos animes retornados estão corretos
        Assert.Equal(animes[0].Director, result.Item1.ElementAt(0).Director);
        Assert.Equal(animes[1].Director, result.Item1.ElementAt(1).Director);
    }
}
