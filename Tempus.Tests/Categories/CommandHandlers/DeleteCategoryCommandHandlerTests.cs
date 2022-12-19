using Moq;
using Tempus.Core.Commands.Categories.Create;
using Tempus.Core.Commands.Categories.Delete;
using Tempus.Core.Commands.Categories.Update;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Category;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Categories.CommandHandlers;

public class DeleteCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly DeleteCategoryCommandHandler _sut;

    public DeleteCategoryCommandHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();

        _sut = new DeleteCategoryCommandHandler(_mockCategoryRepository.Object);
    }

    [Fact]
    public async Task
        Given_DeleteCategoryCommandWithInvalidId_When_HandleDeleteCategoryCommand_ItShould_ReturnNotFound()
    {
        var categoryId = Guid.NewGuid();
        _mockCategoryRepository
            .Setup(x => x.GetById(categoryId))
            .ReturnsAsync((Category?)null);
    
        var expected = BaseResponse<BaseCategory>.NotFound($"Category with Id: {categoryId} not found");
    
        var actual = await _sut.Handle(new DeleteCategoryCommand
            {
                Id = categoryId
            },
            new CancellationToken());
    
        Assert.NotNull(actual);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_CategoryId_When_HandleDeleteCategoryCommand_ItShould_ReturnOk()
    {
        var deletedCategoryId = Guid.NewGuid();
        _mockCategoryRepository
            .Setup(x => x.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(deletedCategoryId);

        var expected = BaseResponse<Guid>.Ok(deletedCategoryId);

        var actual = await _sut.Handle(new DeleteCategoryCommand
        {
            Id = deletedCategoryId
        },
            new CancellationToken());
        
        Assert.Equal(expected.Resource, actual.Resource);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Null(actual.Errors);
    }

    [Fact]
    public async Task When_CancelHandleDeleteCategoryCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new DeleteCategoryCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}