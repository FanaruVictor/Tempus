using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;

namespace Tempus.Core.Commands.Registrations.Create;

public class CreateRegistrationCommand : IRequest<BaseResponse<DetailedRegistration>>
{
    public string? Title { get; init; }
    public string? Content { get; init; }
    public Guid CategoryId { get; init; }
}