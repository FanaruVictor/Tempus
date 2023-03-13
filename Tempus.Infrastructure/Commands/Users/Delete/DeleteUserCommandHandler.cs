using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commands.ProfilePhoto.DeleteProfilePhoto;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.Users.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public DeleteUserCommandHandler(IUserRepository userRepository, ICloudinaryService cloudinaryService)
    {
        _userRepository = userRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<BaseResponse<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);
            
            BaseResponse<Guid> result;

            if (user == null)
            {
                result = BaseResponse<Guid>.NotFound($"User with Id: {request.UserId} not found");
                return result;
            }
            
            var deletedUserId = request.UserId;
            
            await _userRepository.Delete(deletedUserId);

            if (user.ProfilePhoto != null && user.ExternalId == null)
            {
                await _cloudinaryService.DestroyUsingUserId(deletedUserId);
            }

            await _userRepository.SaveChanges();

            result = BaseResponse<Guid>.Ok(deletedUserId);
            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<Guid>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }
}