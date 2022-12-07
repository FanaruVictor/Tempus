using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;

namespace Tempus.Core.Commands.Registrations.Update;

public class UpdateRegistrationCommand : IRequest<BaseResponse<DetailedRegistration>>
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
}