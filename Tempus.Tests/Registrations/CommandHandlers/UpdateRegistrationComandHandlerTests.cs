using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commands.Registrations.Update;
using Tempus.Infrastructure.Commons;

namespace Tempus.Tests.Registrations.CommandHandlers;

public class UpdateRegistrationCommandHandlerTests
{
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly UpdateRegistrationCommandHandler _sut;

    public UpdateRegistrationCommandHandlerTests()
    {
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        _sut = new UpdateRegistrationCommandHandler(_mockRegistrationRepository.Object);
    }

    [Fact]
    public async Task Given_WrongRegisrationId_When_HandleUpdateRegisration_ItShould_ReturnBadRequest()
    {
        var registrationId = Guid.NewGuid();
        _mockRegistrationRepository
            .Setup(x => x.GetById(registrationId))
            .ReturnsAsync((Registration?)null);
        
        var expected = BaseResponse<BaseRegistration>.NotFound($"Category with Id: {registrationId} not found.");


        var actual = await _sut.Handle(new UpdateRegistrationCommand
            {
                Id = registrationId,
                Content = "content",
                Title = "title"
            },
            new CancellationToken());

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }

    [Fact]
    public async Task Given_CommandWithIdExistingInDb_When_HandleUpdateCommand_ItShould_ReturnOK()
    {
        var registration = new Registration(
            Guid.NewGuid(),
            "title",
            "content",
            DateTime.Now,
            DateTime.Now,
            Guid.NewGuid());

        _mockRegistrationRepository
            .Setup(x => x.GetById(registration.Id))
            .ReturnsAsync(registration);

        var updatedRegistration = new Registration(
            registration.Id,
            "new title",
            "new content",
            DateTime.Now, 
            DateTime.Now, 
            registration.CategoryId);
        
        _mockRegistrationRepository
            .Setup(x => x.Update(It.IsAny<Registration>()))
            .ReturnsAsync(updatedRegistration);

        var baseRegistration = GenericMapper<Registration, BaseRegistration>.Map(updatedRegistration);
        var expected = BaseResponse<BaseRegistration>.Ok(baseRegistration);
        
        var actual = await _sut.Handle(new UpdateRegistrationCommand
            {
                Id = registration.Id,
                Title = registration.Title,
                Content = registration.Content
            },
            new CancellationToken());

        Assert.NotNull(actual);
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Content, actual.Resource?.Content);
        Assert.Equal(expected.Resource?.Title, actual.Resource?.Title);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
    }

    [Fact]
    public async Task When_CancelHandleUpdateRegistrationCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new UpdateRegistrationCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}