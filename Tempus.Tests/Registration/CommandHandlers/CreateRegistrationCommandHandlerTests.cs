using Moq;
using Tempus.Core.Commands.Registrations.Create;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registration;
using Tempus.Core.Repositories;

namespace Tempus.Tests.Registration.CommandHandlers;

public class CreateRegistrationCommandHandlerTests
{
    private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private CreateRegistrationCommandHandler _sut;

    public CreateRegistrationCommandHandlerTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        _sut = new CreateRegistrationCommandHandler(_mockRegistrationRepository.Object, _mockCategoryRepository.Object);
    }

    [Fact]
    public async Task GivenCommandWithWrongCategoryId_WhenHandleCreateCommand_ItShould_ReturnBadRequest()
    {
        _mockCategoryRepository
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .Returns(() => null);
        
        var categoryId = Guid.NewGuid();
        var expected = BaseResponse<DetailedRegistration>.BadRequest($"Category with Id: {categoryId} not found");
        
        var actual = await _sut.Handle(
            new CreateRegistrationCommand
            {
                CategoryId = categoryId,
            },
            new CancellationToken()
        );
        
        Assert.NotNull(actual);
        Assert.Equal(actual.Errors, expected.Errors);
        Assert.Equal(actual.StatusCode, expected.StatusCode);
    }
}