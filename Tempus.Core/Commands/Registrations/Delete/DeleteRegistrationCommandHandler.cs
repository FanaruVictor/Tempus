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

            BaseResponse<Guid> result;
            
            if (deletedRegistrationId == Guid.Empty)
            {
                result = BaseResponse<Guid>.NotFound($"Registration with Id: {request.Id} not found");
                return result;
            }
            
            result = BaseResponse<Guid>.Ok(deletedRegistrationId);
            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<Guid>.BadRequest(new List<string>{exception.Message});
        }
    }
}