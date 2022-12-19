using Moq;
using Tempus.Core.Commands.Categories.Update;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Category;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Categories.CommandHandlers;

public class UpdateCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly UpdateCategoryCommandHandler _sut;

    public UpdateCategoryCommandHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();

        _sut = new UpdateCategoryCommandHandler(_mockCategoryRepository.Object);
    }

    [Fact]
    public async Task
        Given_UpdateCategoryCommandWithInvalidId_When_HandleUpdateCategoryCommand_ItShould_ReturnNotFound()
    {
        var categoryId = Guid.NewGuid();
        _mockCategoryRepository
            .Setup(x => x.GetById(categoryId))
            .ReturnsAsync((Category?)null);

        var expected = BaseResponse<BaseCategory>.NotFound($"Category with Id: {categoryId} not found.");

        var actual = await _sut.Handle(new UpdateCategoryCommand
            {
                Id = categoryId,
                Color = "black",
                Name = "category"
            },
            new CancellationToken());

        Assert.NotNull(actual);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_UpdateCategoryCommandData_When_HandleUpdateCategoryCommand_ItShould_ReturnOK()
    {
        var category = new Category(
            Guid.NewGuid(),
            "category",
            DateTime.Now,
            DateTime.Now,
            "black",
            Guid.NewGuid());

        var updatedCategory = new Category(
            category.Id,
            "new category",
            DateTime.Now,
            DateTime.Now,
            "white",
            category.UserId);

        _mockCategoryRepository
            .Setup(x => x.GetById(category.Id))
            .ReturnsAsync(category);
        _mockCategoryRepository
            .Setup(x => x.Update(It.IsAny<Category>()))
            .ReturnsAsync(updatedCategory);

        var baseCategory = GenericMapper<Category, BaseCategory>.Map(updatedCategory);
        var expected = BaseResponse<BaseCategory>.Ok(baseCategory);

        var actual = await _sut.Handle(new UpdateCategoryCommand
            {
                Color = "white",
                Name = "new name",
                Id = category.Id
            },
            new CancellationToken());

        Assert.NotNull(actual);
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Color, actual.Resource?.Color);
        Assert.Equal(expected.Resource?.Name, actual.Resource?.Name);
        Assert.Equal(expected.Resource?.UserId, actual.Resource?.UserId);
    }

    [Fact]
    public async Task When_CancelHandleDeleteCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new UpdateCategoryCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}