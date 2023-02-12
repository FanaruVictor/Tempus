using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commands.Users.Delete;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Tests.Users.CommandHandlers;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly DeleteUserCommandHandler _sut;

    public DeleteUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();

        _sut = new DeleteUserCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task
        Given_DeleteUserCommandWithInvalidId_When_HandleDeleteUserCommand_ItShould_ReturnNotFound()
    {
        var userId = Guid.NewGuid();
        _mockUserRepository
            .Setup(x => x.GetById(userId))
            .ReturnsAsync((User?)null);

        var expected = BaseResponse<BaseUser>.NotFound($"User with Id: {userId} not found");

        var actual = await _sut.Handle(new DeleteUserCommand
            {
                Id = userId
            },
            new CancellationToken());

        Assert.NotNull(actual);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_UserId_When_HandleDeleteUserCommand_ItShould_ReturnOk()
    {
        var deletedUserId = Guid.NewGuid();
        _mockUserRepository
            .Setup(x => x.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(deletedUserId);

        var expected = BaseResponse<Guid>.Ok(deletedUserId);

        var actual = await _sut.Handle(new DeleteUserCommand
            {
                Id = deletedUserId
            },
            new CancellationToken());

        Assert.Equal(expected.Resource, actual.Resource);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Null(actual.Errors);
    }

    [Fact]
    public async Task When_CancelHandleDelteUserCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new DeleteUserCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}