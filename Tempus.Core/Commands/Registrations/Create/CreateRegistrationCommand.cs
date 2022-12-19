using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Core.Commands.Registrations.Create;

public class CreateRegistrationCommand : IRequest<BaseResponse<BaseRegistration>>
{
    public string? Title { get; init; }
    public string? Content { get; init; }
    public Guid CategoryId { get; init; }
}