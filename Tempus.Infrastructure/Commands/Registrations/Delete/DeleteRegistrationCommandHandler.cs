using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Commands.Registrations.Delete;

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

            var registration = !request.GroupId.HasValue
                ? await _registrationRepository.GetById(request.Id)
                : await _registrationRepository.GetById(request.Id, request.GroupId.Value);

            if (registration == null)
            {
                return BaseResponse<Guid>.NotFound("Registration not found!");
            }

            var validator = ValidateRequest(request, registration);

            if (validator.StatusCode != StatusCodes.Ok)
            {
                return validator;
            }

            await _registrationRepository.Delete(registration.Id);
            await _registrationRepository.SaveChanges();

            return BaseResponse<Guid>.Ok(registration.Id);
        }
        catch (Exception exception)
        {
            return BaseResponse<Guid>.BadRequest(new List<string> { exception.Message });
        }
    }

    private BaseResponse<Guid> ValidateRequest(DeleteRegistrationCommand request,
        Registration registration)
    {
        if (request.GroupId.HasValue)
        {
            return ValidateForGroup(request, registration);
        }

        return ValidateForUser(request, registration);
    }

    private BaseResponse<Guid> ValidateForUser(DeleteRegistrationCommand request,
        Registration registration)
    {
        var userId = registration.Category.UserCategories.FirstOrDefault(x => x.CategoryId == registration.Category.Id)
            ?.UserId;

        if (userId == null)
        {
            return BaseResponse<Guid>.BadRequest(new List<string> { "Internal server error" });
        }

        if (userId != request.UserId)
        {
            return BaseResponse<Guid>.Forbbiden();
        }

        return BaseResponse<Guid>.Ok();
    }

    private BaseResponse<Guid> ValidateForGroup(DeleteRegistrationCommand request,
        Registration registration)
    {
        var groupId = registration.Category.GroupCategories
            .FirstOrDefault(x => x.CategoryId == registration.Category.Id)
            ?.GroupId;

        if (groupId == null)
        {
            return BaseResponse<Guid>.NotFound("Group not found!");
        }

        return BaseResponse<Guid>.Ok();
    }
}