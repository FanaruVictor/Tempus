using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Registrations.Delete;

public class DeleteRegistrationCommandHandler : IRequestHandler<DeleteRegistrationCommand, BaseResponse<Guid>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public DeleteRegistrationCommandHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<Guid>> Handle(DeleteRegistrationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var deletedRegistrationId = await _registrationRepository.Delete(request.Id);

            var result = BaseResponse<Guid>.Ok(deletedRegistrationId);
            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<Guid>.BadRequest(exception.Message);
        }
    }
}