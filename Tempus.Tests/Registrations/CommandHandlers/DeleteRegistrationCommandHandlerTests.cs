using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commands.Registrations.Delete;

namespace Tempus.Tests.Registrations.CommandHandlers;

public class DeleteRegistrationCommandHandlerTests
{
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly DeleteRegistrationCommandHandler _sut;

    public DeleteRegistrationCommandHandlerTests()
    {
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        _sut = new DeleteRegistrationCommandHandler(_mockRegistrationRepository.Object);
    }

    [Fact]
    public async Task
        Given_InexistentRegistrationIdInDb_When_HandleDeleteRegistrationCommand_ItShould_ReturnBadRequest()
    {
        _mockRegistrationRepository
            .Setup(x => x.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(Guid.Empty);

        var registrationId = Guid.NewGuid();
        var expected = BaseResponse<Guid>.NotFound($"Registration with Id: {registrationId} not found");

        var actual = await _sut.Handle(new DeleteRegistrationCommand
            {
                Id = registrationId
            }
            , new CancellationToken());

        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }

    [Fact]
    public async Task Given_RegistrationId_When_HandleDeleteRegistrationCommand_ItShould_ReturnOk()
    {
        var deletedRegistrationId = Guid.NewGuid();
        _mockRegistrationRepository
            .Setup(x => x.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(deletedRegistrationId);

        var expected = BaseResponse<Guid>.Ok(deletedRegistrationId);

        var actual = await _sut.Handle(new DeleteRegistrationCommand
        {
            Id = deletedRegistrationId
        }, new CancellationToken());
        
        Assert.Equal(expected.Resource, actual.Resource);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Null(actual.Errors);
    }

    [Fact]
    public async Task When_CancelHandleDeleteRegistrationCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new DeleteRegistrationCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}