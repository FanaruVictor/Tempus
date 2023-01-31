using Tempus.Infrastructure.Commons;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class CreateRegistrationCommand : BaseRequest<BaseResponse<BaseRegistration>>
{
    public string? Title { get; init; }
    public string? Content { get; init; }
    public Guid CategoryId { get; init; }
}