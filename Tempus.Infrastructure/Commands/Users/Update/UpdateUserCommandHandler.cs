using MediatR;
using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;
using Tempus.Infrastructure.Commands.ProfilePhoto.DeleteProfilePhoto;
using Tempus.Infrastructure.Commands.ProfilePhoto.UpdateProfilePhoto;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IProfilePhotoRepository _profilePhotoRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMediator mediator, ICloudinaryService cloudinaryService, IProfilePhotoRepository profilePhotoRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _cloudinaryService = cloudinaryService;
        _profilePhotoRepository = profilePhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);

            if(user == null)
            {
                return BaseResponse<UserDetails>.NotFound($"User with id {request.UserId} not .");
            }
            
            await UpdateUser(request, user);
            
            var userDetails = GenericMapper<User, UserDetails>.Map(user);
            
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
        if (request.IsPhotoChanged)
        {
            UpdatePhoto(request.NewPhoto, user);
        }
        
        user = new User
        {
            Id = user.Id,
            Username = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = user.Password,
            PasswordSalt = user.PasswordSalt,
            IsDarkTheme = user.IsDarkTheme,
            ExternalId = user.ExternalId
        };

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();
    }

    private async Task UpdatePhoto(IFormFile photo, User user)
    {
        if (user.ProfilePhoto != null)
        {
            await _mediator.Send(new UpdateProfilePhotoCommand
            {
                Image = photo,
                UserId = user.Id,
                Id = user.ProfilePhoto.Id 
            });
            return;
        }

        await _mediator.Send(new AddProfilePhotoCommand
        {
            Image = photo,
            UserId = user.Id
        });
    }
}