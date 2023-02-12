using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Registrations;
using Tempus.Infrastructure.Queries.Registrations.GetAll;

namespace Tempus.Tests.Registrations.QueryHandlers;

public class GetAllRegistrationQueryHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly GetAllRegistrationsQueryHandler _sut;

    public GetAllRegistrationQueryHandlerTests()
    {
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();
        _mockCategoryRepository = new Mock<ICategoryRepository>();

        _sut = new GetAllRegistrationsQueryHandler(_mockRegistrationRepository.Object, _mockCategoryRepository.Object);
    }

    [Fact]
    public async Task
        Given_GetAllRegistrationsQueryWithoutCategoryId_When_HandleGetAllRegistrationQuery_ItShould_ReturnOk()
    {
        var registration = new Registration(
            Guid.NewGuid(),
            "title",
            "content",
            DateTime.Now,
            DateTime.Now,
            Guid.NewGuid());

        _mockRegistrationRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(new List<Registration> {registration});

        _mockCategoryRepository
            .Setup(x => x.GetCategoryColor(It.IsAny<Guid>()))
            .Returns("black");

        var expected = BaseResponse<List<DetailedRegistration>>.Ok(
            new List<DetailedRegistration>
            {
                GenericMapper<Registration, DetailedRegistration>.Map(registration)
            });

        var actual = await _sut.Handle(new GetAllRegistrationsQuery(), new CancellationToken());

        Assert.Equal(expected.Resource?.Count, actual.Resource?.Count);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }

    [Fact]
    public async Task Given_GetAllRegistrationQueryWithCategoryId_When_HandleGetAllRegistrationQuery_ItShould_ReturnOk()
    {
        var categoryId = Guid.NewGuid();
        var registrations = new List<Registration>
        {
            new(
                Guid.NewGuid(),
                "title1",
                "content1",
                DateTime.Now,
                DateTime.Now,
                categoryId),
            new(
                Guid.NewGuid(),
                "title2",
                "content2",
                DateTime.Now,
                DateTime.Now,
                Guid.NewGuid())
        };

        _mockRegistrationRepository
            .Setup(x => x.GetAll(categoryId, It.IsAny<Guid>()))
            .ReturnsAsync(registrations.Where(x => x.CategoryId == categoryId).ToList());

        var expected = BaseResponse<List<DetailedRegistration>>.Ok(
            registrations
                .Where(x => x.CategoryId == categoryId)
                .Select(GenericMapper<Registration, DetailedRegistration>.Map)
                .ToList()
        );

        var actual = await _sut.Handle(new GetAllRegistrationsQuery
            {
                CategoryId = categoryId
            },
            new CancellationToken());

        Assert.Equal(expected.Resource?.Count, actual.Resource?.Count);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
    }

    [Fact]
    public async Task When_CancelHandleGetAllCategoriesQuery_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new GetAllRegistrationsQuery(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}