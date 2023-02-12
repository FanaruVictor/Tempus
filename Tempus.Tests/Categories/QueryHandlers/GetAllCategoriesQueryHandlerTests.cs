using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;
using Tempus.Infrastructure.Queries.Categories.GetAll;

namespace Tempus.Tests.Categories.QueryHandlers;

public class GetAllCategoriesQueryHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly GetAllCategoriesQueryHandler _sut;

    public GetAllCategoriesQueryHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();

        _sut = new GetAllCategoriesQueryHandler(_mockCategoryRepository.Object);
    }

    [Fact]
    public async Task
        Given_GetAllCategoriesQueryWithoutUserId_When_HandleGetAllCategoriesQuery_ItShould_ReturnOk()
    {
        var category = new Category(
            Guid.NewGuid(),
            "name",
            DateTime.Now,
            DateTime.Now,
            "color",
            Guid.NewGuid());

        _mockCategoryRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(new List<Category> {category});

        var expected = BaseResponse<List<BaseCategory>>.Ok(
            new List<BaseCategory>
            {
                GenericMapper<Category, BaseCategory>.Map(category)
            });

        var actual = await _sut.Handle(new GetAllCategoriesQuery(), new CancellationToken());

        Assert.Equal(expected.Resource?.Count, actual.Resource?.Count);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }

    [Fact]
    public async Task Given_GetAllCategoriesQueryWithUserId_When_HandleGetAllCategoryQuery_ItShould_ReturnOk()
    {
        var userId = Guid.NewGuid();
        var categories = new List<Category>
        {
            new(
                Guid.NewGuid(),
                "category1",
                DateTime.Now,
                DateTime.Now,
                "color1",
                userId),
            new(
                Guid.NewGuid(),
                "category2",
                DateTime.Now,
                DateTime.Now,
                "color2",
                Guid.NewGuid())
        };

        _mockCategoryRepository
            .Setup(x => x.GetAll(userId))
            .ReturnsAsync(categories.Where(x => x.UserId == userId).ToList());

        var expected = BaseResponse<List<BaseCategory>>.Ok(
            categories
                .Where(x => x.UserId == userId)
                .Select(GenericMapper<Category, BaseCategory>.Map)
                .ToList()
        );

        var actual = await _sut.Handle(new GetAllCategoriesQuery
            {
                UserId = userId
            },
            new CancellationToken());

        Assert.Equal(expected.Resource?.Count, actual.Resource?.Count);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }

    [Fact]
    public async Task When_CancelHandleGetAllCategoriesQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetAllCategoriesQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}