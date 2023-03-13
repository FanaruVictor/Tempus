using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private IProfilePhotoRepository _profilePhotoRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IProfilePhotoRepository profilePhotoRepository)
    {
        _userRepository = userRepository;
        _profilePhotoRepository = profilePhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.Id);

            if(user == null)
            {
                return BaseResponse<UserDetails>.NotFound($"User with id {request.Id} not .");
            }
            
            await UpdateUser(request, user);

            var profilePhoto = await _profilePhotoRepository.GetByUserId(user.Id);
            
            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            if(profilePhoto != null)
            {
                userDetails.Photo = GenericMapper<Core.Entities.ProfilePhoto, PhotoDetails>.Map(profilePhoto);
            }
            var result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string> {exception.Message});
        }
    }

    private async Task UpdateUser(UpdateUserCommand request, User user)
    {
        user = new User
        {
            Id = user.Id,
            Username = request.UserName,
            Email = request.Email
        };

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();
    }
}