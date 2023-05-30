using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;
using StatusCodes = Tempus.Core.Commons.StatusCodes;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserPhotoRepository _userPhotoRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMediator mediator,
        ICloudinaryService cloudinaryService, IUserPhotoRepository userPhotoRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _cloudinaryService = cloudinaryService;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);

            if (user == null)
            {
                return BaseResponse<UserDetails>.NotFound($"User with id {request.UserId} not .");
            }

            var updateResult = await UpdateUser(request, user);

            BaseResponse<UserDetails> result;
            if (updateResult.StatusCode != StatusCodes.Ok)
            {
                result = new BaseResponse<UserDetails>
                {
                    Errors = updateResult.Errors,
                    StatusCode = updateResult.StatusCode
                };
                return result;
            }

            await _userRepository.SaveChanges();

            var userDetails = GenericMapper<User, UserDetails>.Map(updateResult.Resource);
            userDetails.Photo = GenericMapper<Core.Entities.User.UserPhoto, PhotoDetails>.Map(updateResult.Resource.UserPhoto);

            result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string> { exception.Message });
        }
    }

    private async Task<BaseResponse<User>> UpdateUser(UpdateUserCommand request, User user)
    {
        user = new User
        {
            Id = user.Id,
            Username = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = user.Password,
            PasswordSalt = user.PasswordSalt,
            IsDarkTheme = user.IsDarkTheme,
            ExternalId = user.ExternalId,
            UserPhoto = user.UserPhoto
        };
        
        if (request.IsPhotoChanged)
        {
            var updatePhotoResult = await UpdatePhoto(request.NewPhoto, user);

            if (updatePhotoResult.StatusCode != StatusCodes.Ok)
            {
                return new BaseResponse<User>
                {
                    Errors = updatePhotoResult.Errors,
                    StatusCode = updatePhotoResult.StatusCode,
                };
            }

            user.UserPhoto = updatePhotoResult.Resource ?? null;
        }

        _userRepository.Update(user);
        return BaseResponse<User>.Ok(user);
    }

    private async Task<BaseResponse<Core.Entities.User.UserPhoto>> UpdatePhoto(IFormFile? photo, User user)
    {
        if (photo == null)
        {
            if (user.UserPhoto != null)
            {
                await _cloudinaryService.DestroyUsingUserId(user.Id);
                await _userPhotoRepository.Delete(user.UserPhoto.Id);
            }

            return BaseResponse<Core.Entities.User.UserPhoto>.Ok();
        }

        ImageUploadResult uploadResult;
        Core.Entities.User.UserPhoto userUserPhoto;
        if (user.UserPhoto != null)
        {
            await _cloudinaryService.DestroyUsingUserId(user.Id);
            uploadResult = await _cloudinaryService.Upload(photo);
            userUserPhoto = new Core.Entities.User.UserPhoto
            {
                Id = user.UserPhoto.Id,
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString(),
                UserId = user.Id
            };
            _userPhotoRepository.Update(userUserPhoto);
        }
        else
        {
            uploadResult = await _cloudinaryService.Upload(photo);
            userUserPhoto = new Core.Entities.User.UserPhoto
            {
                Id = Guid.NewGuid(),
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString(),
                UserId = user.Id
            };
            await _userPhotoRepository.Add(userUserPhoto);
        }

        await _userPhotoRepository.SaveChanges();

        return BaseResponse<Core.Entities.User.UserPhoto>.Ok(userUserPhoto);
    }
}