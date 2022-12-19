using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Queries.Registrations.GetById;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Registrations.QueryHandlers;

public class GetRegistrationByIdQueryHandlerTests
{
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly GetRegistrationByIdQueryHandler _sut;

    public GetRegistrationByIdQueryHandlerTests()
    {
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        _sut = new GetRegistrationByIdQueryHandler(_mockRegistrationRepository.Object);
    }

    [Fact]
    public async Task Given_InexistentRegistrationIdInDb_When_HandleGetRegistrationByIdQuery_ItShould_ReturnNotFound()
    {
        _mockRegistrationRepository
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Registration?)null);

        var expected = BaseResponse<BaseRegistration>.NotFound("Registration not found!");

        var actual = await _sut.Handle(new GetRegistrationByIdQuery(), new CancellationToken());

        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
    }

    [Fact]
    public async Task Given_RegistrationId_When_HandleGetRegistrationByIdQuery_ItShould_ReturnOk()
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

        var baseRegistration = GenericMapper<Registration, BaseRegistration>.Map(registration);
        var expected = BaseResponse<BaseRegistration>.Ok(baseRegistration);

        var actual = await _sut.Handle(new GetRegistrationByIdQuery
            {
                Id = registration.Id
            },
            new CancellationToken());
        
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.Content, actual.Resource?.Content);
        Assert.Equal(expected.Resource?.Title, actual.Resource?.Title);
    }

    [Fact]
    public async Task When_CancelHandleGetRegistrationByIdQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetRegistrationByIdQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}