using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;
using Tempus.Infrastructure.Models.User;
using Tempus.Infrastructure.Queries.Users.GetById;

namespace Tempus.Tests.Users.QueryHandlers;

public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IProfilePhotoRepository> _mockProfilePhotoRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly GetUserByIdQueryHandler _sut;

    public GetUserByIdQueryHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockProfilePhotoRepository = new Mock<IProfilePhotoRepository>();

        _sut = new GetUserByIdQueryHandler(_mockUserRepository.Object, _mockProfilePhotoRepository.Object);
    }

    [Fact]
    public async Task Given_InexistentUserIdInDb_When_HandleGetUserByIdQuery_ItShould_ReturnNotFound()
    {
        _mockUserRepository
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((User?)null);

        var expected = BaseResponse<BaseCategory>.NotFound("User not found.");

        var actual = await _sut.Handle(new GetUserByIdQuery(), new CancellationToken());

        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_RegistrationId_When_HandleGetRegistrationByIdQuery_ItShould_ReturnOk()
    {
        var user = new User(Guid.NewGuid(), "username", "email");

        _mockUserRepository
            .Setup(x => x.GetById(user.Id))
            .ReturnsAsync(user);

        var baseUser = GenericMapper<User, BaseUser>.Map(user);
        var expected = BaseResponse<BaseUser>.Ok(baseUser);

        var actual = await _sut.Handle(new GetUserByIdQuery()
            ,
            new CancellationToken());

        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.UserName, actual.Resource?.UserName);
        Assert.Equal(expected.Resource?.Email, actual.Resource?.Email);
    }

    [Fact]
    public async Task When_CancelHandleGetAllCategoriesQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetUserByIdQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}