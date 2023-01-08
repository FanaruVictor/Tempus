using MediatR;
using Tempus.Core.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Delete;

public class DeleteRegistrationCommand : IRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}