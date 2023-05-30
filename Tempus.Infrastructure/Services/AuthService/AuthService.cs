using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tempus.Core.Commons;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.IServices;
using Tempus.Core.Models.Auth;
using Tempus.Core.Models.Photo;
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

            if(!await _authRepository.IsEmailAlreadyRegistered(credentials.Email))
            {
                throw new Exception("User not found");
            }

            var user = await _authRepository.Login(credentials.Email, credentials.Password);

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

    public async Task<BaseResponse<UserDetails>> Register(RegistrationData userInfo,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            BaseResponse<UserDetails> result;


            if(await _authRepository.IsEmailAlreadyRegistered(userInfo.Email.ToLower()))
            {
                throw new Exception("Email already registered");
            }

            if(await _authRepository.IsUsernameAlreadyRegistered(userInfo.UserName.ToLower()))
            {
                throw new Exception("Username already registered");
            }

            var entity = await RegisterUser(userInfo);

            var baseUser = GenericMapper<User, UserDetails>.Map(entity);
            result = BaseResponse<UserDetails>.Ok(baseUser);

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

    public async Task<BaseResponse<LoginResult>> LoginWithGoogle(string googleCredentials,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            BaseResponse<LoginResult> result;

            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> {_configuration["GoogleSettings:ClientId"]}
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(googleCredentials, settings);

            if(payload == null)
            {
                result = BaseResponse<LoginResult>.BadRequest(new List<string>
                {
                    "User credentials not right"
                });

                return result;
            }

            var user = await _userRepository.GetByExternalId(payload.Subject);

            if(user == null)
            {
                var registrationData = new ExternalRegistration
                {
                    ExternalId = payload.Subject,
                    Email = payload.Email,
                    Username = payload.Name,
                    PhotoUrl = payload.Picture
                };
                
                user = await RegisterExternalUser(registrationData);
            }

            CreateToken(user, out var tokenHandler, out var token);

            var loginResult = new LoginResult
            {
                User = GenericMapper<User, UserDetails>.Map(user),
                AuthorizationToken = tokenHandler.WriteToken(token)
            };


            return BaseResponse<LoginResult>.Ok(loginResult);
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

    public async Task<BaseResponse<LoginResult>> LoginWithFacebook(FacebookResponse facebookResponse, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalId(facebookResponse.ExternalId);

        if(user == null)
        {
            var registrationData = new ExternalRegistration
            {
                ExternalId = facebookResponse.ExternalId,
                Email = facebookResponse.Email,
                Username = facebookResponse.Username,
                PhotoUrl = facebookResponse.PhotoUrl
            };
            
            user = await RegisterExternalUser(registrationData);
        }
        
        CreateToken(user, out var tokenHandler, out var token);

        var loginResult = new LoginResult
        {
            User = GenericMapper<User, UserDetails>.Map(user),
            AuthorizationToken = tokenHandler.WriteToken(token)
        };
        
        loginResult.User.Photo = GenericMapper<UserPhoto, PhotoDetails>.Map(user.UserPhoto);


        return BaseResponse<LoginResult>.Ok(loginResult);
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

        await _authRepository.Register(user, userInfo.Password);
        await _authRepository.SaveChanges();
        return user;
    }


    private async Task<User> RegisterExternalUser(ExternalRegistration data)
    {
        var user = await RegisterUser(new RegistrationData
        {
            Email = data.Email,
            ExternalId = data.ExternalId,
            UserName = data.Username
        });

        var photo = new UserPhoto
        {
            Id = Guid.NewGuid(),
            Url = data.PhotoUrl,
            PublicId = "",
            UserId = user.Id,
        };

        await _userPhotoRepository.Add(photo);
        await _userPhotoRepository.SaveChanges();

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