using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Delete;

public class DeleteRegistrationCommand : BaseRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}