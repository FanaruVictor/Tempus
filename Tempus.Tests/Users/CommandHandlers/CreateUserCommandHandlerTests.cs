using Moq;
using Tempus.Core.Commands.Registrations.Create;
using Tempus.Core.Commands.Users.Create;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Users.CommandHandlers;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly CreateUserCommandHandler _sut;

    public CreateUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();

        _sut = new CreateUserCommandHandler(_mockUserRepository.Object);
    }
    
    [Fact]
    public async Task Given_CommandWithUserData_When_HandleCreateUserCommand_ItShould_ReturnOk()
    {
        var user = new User(Guid.NewGuid(), "username", "email");
        _mockUserRepository
            .Setup(x => x.Add(It.IsAny<User>()))
            .ReturnsAsync(user);

        var baseUser = GenericMapper<User, BaseUser>.Map(user);
        var expected = BaseResponse<BaseUser>.Ok(baseUser);

        var actual = await _sut.Handle(new CreateUserCommand
            {
                UserName = user.UserName,
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

        var actual = await _sut.Handle(new CreateUserCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}