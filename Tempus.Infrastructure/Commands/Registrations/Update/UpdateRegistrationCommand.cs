using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class UpdateRegistrationCommand : IRequest<BaseResponse<BaseRegistration>>
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
}