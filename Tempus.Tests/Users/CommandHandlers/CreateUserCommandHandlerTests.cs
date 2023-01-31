using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commands.Auth.Register;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Tests.Users.CommandHandlers;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IAuthRepository> _mockAuthRepository;
    private readonly RegisterUserCommandHandler _sut;

    public CreateUserCommandHandlerTests()
    {
        _mockAuthRepository = new Mock<IAuthRepository>();

        _sut = new RegisterUserCommandHandler(_mockAuthRepository.Object);
    }
    
    [Fact]
    public async Task Given_CommandWithUserData_When_HandleCreateUserCommand_ItShould_ReturnOk()
    {
        var user = new User(Guid.NewGuid(), "username", "email");
        _mockAuthRepository
            .Setup(x => x.Register(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        var baseUser = GenericMapper<User, BaseUser>.Map(user);
        var expected = BaseResponse<BaseUser>.Ok(baseUser);

        var actual = await _sut.Handle(new RegisterUserCommand
            {
                UserName = user.Username,
                Email = user.Email
            },
            new CancellationToken());
        
        Assert.NotNull(actual);
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Email, actual.Resource?.Email);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.UserName, actual.Resource?.UserName);
    }

    [Fact]
    public async Task
        When_CancelHandleCreateUserCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new RegisterUserCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}