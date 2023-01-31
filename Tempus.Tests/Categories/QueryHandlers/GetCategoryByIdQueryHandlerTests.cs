using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;
using Tempus.Infrastructure.Queries.Categories.GetById;

namespace Tempus.Tests.Categories.QueryHandlers;

public class GetCategoryByIdQueryHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly GetCategoryByIdQueryHandler _sut;

    public GetCategoryByIdQueryHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();

        _sut = new GetCategoryByIdQueryHandler(_mockCategoryRepository.Object);
    }
    
    [Fact]
    public async Task Given_InexistentCategoryIdInDb_When_HandleGetCategoryByIdQuery_ItShould_ReturnNotFound()
    {
        _mockCategoryRepository
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Category?)null);

        var expected = BaseResponse<BaseCategory>.NotFound("Category not found.");

        var actual = await _sut.Handle(new GetCategoryByIdQuery(), new CancellationToken());

        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_CategoryId_When_HandleGetRegistrationByIdQuery_ItShould_ReturnOk()
    {
        var category = new Category(
            Guid.NewGuid(),
            "name",
            DateTime.Now,
            DateTime.Now,
            "color",
            Guid.NewGuid());

        _mockCategoryRepository
            .Setup(x => x.GetById(category.Id))
            .ReturnsAsync(category);

        var baseCategory = GenericMapper<Category, BaseCategory>.Map(category);
        var expected = BaseResponse<BaseCategory>.Ok(baseCategory);

        var actual = await _sut.Handle(new GetCategoryByIdQuery()
            {
                Id = category.Id
            },
            new CancellationToken());
        
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.Name, actual.Resource?.Name);
        Assert.Equal(expected.Resource?.Color, actual.Resource?.Color);
        Assert.Equal(expected.Resource?.LastUpdatedAt, actual.Resource?.LastUpdatedAt);
        Assert.Equal(expected.Resource?.UserId, actual.Resource?.UserId);
    }
    
    [Fact]
    public async Task When_CancelHandleGetAllCategoriesQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetCategoryByIdQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
    
}