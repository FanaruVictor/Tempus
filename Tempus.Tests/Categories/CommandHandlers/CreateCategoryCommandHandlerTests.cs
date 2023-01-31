using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commands.Categories.Create;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;

namespace Tempus.Tests.Categories.CommandHandlers;

public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly CreateCategoryCommandHandler _sut;

    public CreateCategoryCommandHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockUserRepository = new Mock<IUserRepository>();

        _sut = new CreateCategoryCommandHandler(_mockCategoryRepository.Object, _mockUserRepository.Object);
    }

    [Fact]
    public async Task
        Given_CreateCategoryCommandWithInvalidUserId_When_HandleCreateCategoryCommand_ItShould_ReturnBadRequest()
    {
        var userId = Guid.NewGuid();
        _mockUserRepository
            .Setup(x => x.GetById(userId))
            .ReturnsAsync((User?)null);

        var expected = BaseResponse<BaseCategory>.BadRequest(new List<string>{$"User with Id: {userId} not found"});

        var actual = await _sut.Handle(new CreateCategoryCommand
            {
                UserId = userId,
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
    public async Task Given_CreateCategoryCommandData_When_HandleCreateCategoryCommand_ItShould_ReturnOK()
    {
        var category = new Category(
            Guid.NewGuid(),
            "category",
            DateTime.Now,
            DateTime.Now,
            "black",
            Guid.NewGuid());
        
        _mockCategoryRepository
            .Setup(x => x.Add(It.IsAny<Category>()))
            .ReturnsAsync(category);

        var user = new User(category.UserId, "username", "email");
        
        _mockUserRepository
            .Setup(x => x.GetById(category.UserId))
            .ReturnsAsync(user);

        var baseCategory = GenericMapper<Category, BaseCategory>.Map(category);
        var expected = BaseResponse<BaseCategory>.Ok(baseCategory);

        var actual = await _sut.Handle(new CreateCategoryCommand
            {
                Color = category.Color,
                Name = category.Name,
                UserId = category.UserId
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
    public async Task
        Given_CreateCategoryCommandData_When_CancelHandleCreateCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new CreateCategoryCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}