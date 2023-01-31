using Tempus.Infrastructure.Commons;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class UpdateRegistrationCommand : BaseRequest<BaseResponse<BaseRegistration>>
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
}