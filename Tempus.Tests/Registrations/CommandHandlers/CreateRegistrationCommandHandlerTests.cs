using Moq;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commands.Registrations.Create;
using Tempus.Infrastructure.Commons;

namespace Tempus.Tests.Registrations.CommandHandlers;

public class CreateRegistrationCommandHandlerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly CreateRegistrationCommandHandler _sut;

    public CreateRegistrationCommandHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        _sut = new CreateRegistrationCommandHandler(_mockRegistrationRepository.Object, _mockCategoryRepository.Object);
    }

    [Fact]
    public async Task Given_CommandWithWrongCategoryId_When_HandleCreateCommand_ItShould_ReturnBadRequest()
    {
        _mockCategoryRepository
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Category?)null);

        var categoryId = Guid.NewGuid();
        var expected = BaseResponse<BaseRegistration>.BadRequest(new List<string>{$"Category with Id: {categoryId} not found"});

        var actual = await _sut.Handle(
            new CreateRegistrationCommand
            {
                CategoryId = categoryId,
                Content = "content",
                Title = "title"
            },
            new CancellationToken()
        );

        Assert.NotNull(actual);
        Assert.Equal(actual.Errors, expected.Errors);
        Assert.Equal(actual.StatusCode, expected.StatusCode);
    }

    [Fact]
    public async Task Given_CommandWithCategoryIdExistingInDb_When_HandleCreateRegistrationCommand_ItShould_ReturnOk()
    {
        var categoryId = Guid.NewGuid();
        _mockCategoryRepository
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(new Category(
                categoryId,
                "category",
                DateTime.Now,
                DateTime.Now,
                "color",
                Guid.NewGuid()));

        var registration = new Registration(Guid.NewGuid(), "title", "content", DateTime.Now, DateTime.Now, categoryId);

        var baseRegistration = GenericMapper<Registration, BaseRegistration>.Map(registration);
        var expected = BaseResponse<BaseRegistration>.Ok(baseRegistration);
        
        _mockRegistrationRepository
            .Setup(x => x.Add(It.IsAny<Registration>()))
            .ReturnsAsync(registration);

        var actual = await _sut.Handle(
            new CreateRegistrationCommand
            {
                CategoryId = categoryId,
                Content = registration.Content,
                Title = registration.Title
            }, 
            new CancellationToken());
        
        Assert.NotNull(actual);
        Assert.Equal(expected.Errors, actual.Errors);
        Assert.Equal(expected.StatusCode, actual.StatusCode);
        Assert.Equal(expected.Resource?.Content, actual.Resource?.Content);
        Assert.Equal(expected.Resource?.Id, actual.Resource?.Id);
        Assert.Equal(expected.Resource?.Title, actual.Resource?.Title);
    }

    [Fact]
    public async Task When_CancelHandleCreateRegistrationCommand_ItShouldReturnBadRequest()
    {
        CancellationTokenSource cts = new();
        var cancellationToken = cts.Token;

        cts.Cancel();

        var actual = await _sut.Handle(new CreateRegistrationCommand(), cancellationToken);

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Errors?.Count);
    }
}