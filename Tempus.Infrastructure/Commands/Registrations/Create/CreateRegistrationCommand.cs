using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class CreateRegistrationCommand : BaseRequest<BaseResponse<RegistrationOverview>>
{
    public string? Description { get; init; }
    public string? Content { get; init; }
    public Guid CategoryId { get; init; }
}