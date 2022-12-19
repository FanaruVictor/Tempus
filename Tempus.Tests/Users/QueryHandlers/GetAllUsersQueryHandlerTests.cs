using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Category;
using Tempus.Core.Models.User;
using Tempus.Core.Queries.Categories.GetAll;
using Tempus.Core.Queries.Users.GetAll;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Users.QueryHandlers;

public class GetAllUsersQueryHandlerTests
{
     private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly GetAllUsersQueryHandler _sut;

    public GetAllUsersQueryHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();

        _sut = new GetAllUsersQueryHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task When_HandleGetAllUsersQuery_ItShould_ReturnOk()
    {
        var user = new User(Guid.NewGuid(), "username", "email");

        _mockUserRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(new List<User> { user });

        var expected = BaseResponse<List<BaseUser>>.Ok(
            new List<BaseUser>
            {
                GenericMapper<User, BaseUser>.Map(user)
            });

        var actual = await _sut.Handle(new GetAllUsersQuery(), new CancellationToken());

        Assert.NotNull(actual);
        Assert.Equal(expected.Resource?.Count, actual.Resource?.Count);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }
    
    [Fact]
    public async Task When_CancelHandleGetAllCategoriesQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetAllUsersQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}