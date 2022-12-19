using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Queries.Registrations.GetById;
using Tempus.Core.Queries.Registrations.LastUpdated;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Registrations.QueryHandlers;

public class GetLastRegistrationUpdatedQueryHandlerTests
{
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly GetLastRegistrationUpdatedQueryHandler _sut;

    public GetLastRegistrationUpdatedQueryHandlerTests()
    {
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        _sut = new GetLastRegistrationUpdatedQueryHandler(_mockRegistrationRepository.Object);
    }

    [Fact]
    public async Task When_HandleGetLastRegistrationUpdatedQuery_ItShould_ReturnOk()
    {
        var registration = new Registration(
            Guid.NewGuid(),
            "title",
            "content",
            DateTime.Now,
            DateTime.Now,
            Guid.NewGuid());

        _mockRegistrationRepository
            .Setup(x => x.GetLastUpdated())
            .ReturnsAsync(registration);

        var baseRegistration = GenericMapper<Registration, BaseRegistration>.Map(registration);
        var expected = BaseResponse<BaseRegistration>.Ok(baseRegistration);

        var actual = await _sut.Handle(new GetLastUpdatedRegsitrationQuery(), new CancellationToken());
        
        
        Assert.NotNull(actual);
        Assert.Null(actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.Content, actual.Resource?.Content);
        Assert.Equal(expected.Resource?.Title, actual.Resource?.Title);
    }

    [Fact]
    public async Task When_ThereIsNoRegistrationInDatabaseAndHandleGetLastRegistrationUpdatedQuery_ItShould_ReturnNotFound()
    {
        _mockRegistrationRepository
            .Setup(x => x.GetLastUpdated())
            .ReturnsAsync((Registration?)null);

        var expected = BaseResponse<BaseRegistration>.NotFound("Registration not found!");

        var actual = await _sut.Handle(new GetLastUpdatedRegsitrationQuery(), new CancellationToken());

        Assert.NotNull(actual);
        Assert.Equal(expected.Errors?.Count, actual.Errors?.Count);
        Assert.Equal(expected.Errors?[0], actual.Errors?[0]);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }
    
    [Fact]
    public async Task When_CancelHandleGetLastRegistarationUpdatedQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetLastUpdatedRegsitrationQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}