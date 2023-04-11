using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.GetById;

public class
    GetRegistrationByIdQueryHandler : IRequestHandler<GetRegistrationByIdQuery, BaseResponse<RegistrationDetails>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationByIdQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<RegistrationDetails>> Handle(GetRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var registration = !request.GroupId.HasValue
                ? await _registrationRepository.GetById(request.Id)
                : await _registrationRepository.GetById(request.Id, request.GroupId.Value);

            if (registration == null)
            {
                return BaseResponse<RegistrationDetails>.NotFound("Registration not found!");
            }

            var validator = ValidateRequest(request, registration);

            if (validator.StatusCode != StatusCodes.Ok)
            {
                return validator;
            }

            var response =
                BaseResponse<RegistrationDetails>.Ok(
                    GenericMapper<Registration, RegistrationDetails>.Map(registration));
            return response;
        }
        catch (Exception exception)
        {
            var response = BaseResponse<RegistrationDetails>.BadRequest(new List<string> { exception.Message });
            return response;
        }
    }

    private BaseResponse<RegistrationDetails> ValidateRequest(GetRegistrationByIdQuery request,
        Registration registration)
    {
        if (request.GroupId.HasValue)
        {
            return ValidateForGroup(request, registration);
        }

        return ValidateForUser(request, registration);
    }

    private BaseResponse<RegistrationDetails> ValidateForUser(GetRegistrationByIdQuery request,
        Registration registration)
    {
        var userId = registration.Category.UserCategories.FirstOrDefault(x => x.CategoryId == registration.Category.Id)
            ?.UserId;

        if (userId == null)
        {
            return BaseResponse<RegistrationDetails>.BadRequest(new List<string> { "Internal server error" });
        }

        if (userId != request.UserId)
        {
            return BaseResponse<RegistrationDetails>.Forbbiden();
        }

        return BaseResponse<RegistrationDetails>.Ok();
    }

    private BaseResponse<RegistrationDetails> ValidateForGroup(GetRegistrationByIdQuery request,
        Registration registration)
    {
        var groupId = registration.Category.GroupCategories
            .FirstOrDefault(x => x.CategoryId == registration.Category.Id)
            ?.GroupId;

        if (groupId == null)
        {
            return BaseResponse<RegistrationDetails>.NotFound("Group not found!");
        }

        return BaseResponse<RegistrationDetails>.Ok();
    }
}