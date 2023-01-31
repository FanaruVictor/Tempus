using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commands.Users.Update;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Tests.Users.CommandHandlers;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UpdateUserCommandHandler _sut;

    public UpdateUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();

        _sut = new UpdateUserCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Given_UpdateUserCommandWithInvalidUserId_When_HandleUpdateUserCommand_ItShould_ReturnNotFound()
    {
        var userId = Guid.NewGuid();
        _mockUserRepository
            .Setup(x => x.GetById(userId))
            .ReturnsAsync((User?)null);

        var expected = BaseResponse<BaseUser>.NotFound($"User with id {userId} not .");

        var actual = await _sut.Handle(new UpdateUserCommand
            {
                Id = userId,
                UserName = "username",
                Email = "email"
            },
            new CancellationToken());

        Assert.NotNull(actual);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_UpdateUserCommandData_When_HandleUpdateUserCommand_ItShould_ReturnOk()
    {
        var user = new User(Guid.NewGuid(), "username", "email");
        var updatedUser = new User(user.Id, "new username", "new email");
        _mockUserRepository
            .Setup(x => x.GetById(user.Id))
            .ReturnsAsync(user);
        _mockUserRepository
            .Setup(x => x.Update(It.IsAny<User>()))
            .ReturnsAsync(updatedUser);

        var baseUser = GenericMapper<User, BaseUser>.Map(updatedUser);
        var expected = BaseResponse<BaseUser>.Ok(baseUser);

        var actual = await _sut.Handle(new UpdateUserCommand
        {
            Id = updatedUser.Id,
            UserName = updatedUser.Username,
            Email = updatedUser.Email
        }, new CancellationToken());

        Assert.NotNull(actual);
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.UserName, actual.Resource?.UserName);
        Assert.Equal(expected.Resource?.Email, actual.Resource?.Email);
    }

    [Fact]
    public async Task
        When_CancelHandleCreateRegistrationCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new UpdateUserCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}