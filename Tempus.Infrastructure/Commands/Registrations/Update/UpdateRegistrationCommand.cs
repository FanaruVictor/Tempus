using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class UpdateRegistrationCommand : BaseRequest<BaseResponse<RegistrationDetails>>
{
    public Guid Id { get; init; }
    public string? Description { get; init; }
    public string? Content { get; init; }
}