using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
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

    public AuthService(IAuthRepository authRepository, IConfiguration configuration,
        IUserPhotoRepository userPhotoRepository)
    {
        _authRepository = authRepository;
        _configuration = configuration;
        _userPhotoRepository = userPhotoRepository;
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

            var user = await _authRepository.Login(credentials.Email);

            CreateToken(user, out var tokenHandler, out var token);

            var response = BaseResponse<LoginResult>.Ok(new LoginResult
            {
                UserId = user.Id,
                AuthorizationToken = tokenHandler.WriteToken(token),
                IsDarkTheme = user.IsDarkTheme,
                PhotoURL = user.UserPhoto?.Url
            });

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

            var entity = await RegisterUser(userInfo);

            if(!string.IsNullOrEmpty(entity.ExternalId))
            {
                await AddPhoto(entity, userInfo.PhotoURL);
            }

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
    
    private async Task<User> RegisterUser(RegistrationData userInfo)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = userInfo.Email,
            ExternalId = userInfo.ExternalId,
            IsDarkTheme = false
        };

        await _authRepository.Register(user);
        await _authRepository.SaveChanges();
        return user;
    }

    private async Task AddPhoto(User user, string photoURL)
    {
        var photo = new UserPhoto
        {
            Id = Guid.NewGuid(),
            Url = photoURL,
            PublicId = "",
            UserId = user.Id
        };
        
        await _userPhotoRepository.Add(photo);
    }

    private void CreateToken(User user, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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