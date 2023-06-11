using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tempus.Core;
using Tempus.Core.Commons;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.IServices;
using Tempus.Core.IServices.Auth;
using Tempus.Core.Models;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;
    private readonly IUserPhotoRepository _userPhotoRepository;
    private readonly IUserRepository _userRepository;

    public AuthService(IAuthRepository authRepository, IConfiguration configuration,
        IUserPhotoRepository userPhotoRepository, IUserRepository userRepository)
    {
        _authRepository = authRepository;
        _configuration = configuration;
        _userPhotoRepository = userPhotoRepository;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<LoginResult>> Login(LoginCredentials credentials,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(!await _authRepository.IsEmailAlreadyRegistered(credentials.Email) && !await _authRepository.IsExternalIdAlreadyRegistered(credentials.ExternalId))
            {
                await Register(new RegistrationData
                {
                    Email = credentials.Email,
                    UserName = credentials.UserName,
                    ExternalId = credentials.ExternalId,
                    PhoneNumber = credentials.PhoneNumber,
                    PhotoURL = credentials.PhotoURL
                }, cancellationToken);
            }

            var user = await _authRepository.Login(credentials.Email, credentials.ExternalId);

            CreateToken(user, out var tokenHandler, out var token);

            var result = new LoginResult
            {
                User = GenericMapper<User, UserDetails>.Map(user),
                AuthorizationToken = tokenHandler.WriteToken(token)
            };

            var profilePhoto = await _userPhotoRepository.GetByUserId(user.Id);

            if(profilePhoto != null)
            {
                result.User.Photo = GenericMapper<UserPhoto, PhotoDetails>.Map(profilePhoto);
            }

            var response = BaseResponse<LoginResult>.Ok(result);

            return response;
        }
        catch(TaskCanceledException canceledException)
        {
            return BaseResponse<LoginResult>.BadRequest(new List<string> {canceledException.Message});
        }
        catch(Exception exception)
        {
            var response = BaseResponse<LoginResult>.BadRequest(new List<string> {exception.Message});
            return response;
        }
    }

    private async Task<BaseResponse<UserDetails>> Register(RegistrationData userInfo,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(await _authRepository.IsEmailAlreadyRegistered(userInfo.Email.ToLower()))
            {
                throw new Exception("Email already registered");
            }

            var entity = await RegisterUser(userInfo);

            if(userInfo.PhotoURL != "")
            {
                await _userPhotoRepository.Add(new UserPhoto
                {
                    Id = Guid.NewGuid(),
                    UserId = entity.Id,
                    PublicId = "",
                    Url = userInfo.PhotoURL,
                });
            }

            await _authRepository.SaveChanges();

            var baseUser = GenericMapper<User, UserDetails>.Map(entity);
            var result = BaseResponse<UserDetails>.Ok(baseUser);

            return result;
        }
        catch(TaskCanceledException canceledException)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string> {canceledException.Message});
        }
        catch(Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string> {exception.Message});
        }
    }

   

    private async Task<User> RegisterUser(RegistrationData userInfo)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = userInfo.UserName,
            Email = userInfo.Email,
            PhoneNumber = userInfo.PhoneNumber,
            ExternalId = userInfo.ExternalId
        };

        await _authRepository.Register(user);
        return user;
    }


 
    private void CreateToken(User user, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        tokenHandler = new JwtSecurityTokenHandler();

        token = tokenHandler.CreateToken(tokenDescriptor);
    }
}