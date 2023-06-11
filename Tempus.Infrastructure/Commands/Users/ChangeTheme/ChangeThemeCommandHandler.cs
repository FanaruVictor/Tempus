using MediatR;
using Tempus.Core;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.Models;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Users.ChangeTheme;

public class ChangeThemeCommandHandler : IRequestHandler<ChangeThemeCommand, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private IUserPhotoRepository _userPhotoRepository;

    public ChangeThemeCommandHandler(IUserRepository userRepository, IUserPhotoRepository userPhotoRepository)
    {
        _userRepository = userRepository;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(ChangeThemeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);

            BaseResponse<UserDetails> result;

            if(user == null)
            {
                result = BaseResponse<UserDetails>.NotFound("User not found");

                return result;
            }

            user.IsDarkTheme = request.IsDarkTheme;

            _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var profilePhoto = await _userPhotoRepository.GetByUserId(user.Id);
            
            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            if(profilePhoto != null)
            {
                userDetails.Photo = GenericMapper<Core.Entities.User.UserPhoto, PhotoDetails>.Map(profilePhoto);
            }


            result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }
}