using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class CreateRegistrationCommand : IRequest<BaseResponse<BaseRegistration>>
{
    public string? Title { get; init; }
    public string? Content { get; init; }
    public Guid CategoryId { get; init; }
}