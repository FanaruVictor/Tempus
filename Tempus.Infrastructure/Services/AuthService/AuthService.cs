using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Auth;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }
    
    public async Task<BaseResponse<string>> Login(LoginCredentials credentials, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            BaseResponse<string> response;
            
            if(!await _authRepository.UserExists(credentials.UserName))
            {
                throw new Exception("User not found");
            }
            
            var user = await _authRepository.Login(credentials.UserName, credentials.Password);
            
            CreateToken(user, out var tokenHandler, out var token);

            response = BaseResponse<string>.Ok(tokenHandler.WriteToken(token));

            return response;
        }
        catch(TaskCanceledException canceledException)
        {
            return BaseResponse<string>.BadRequest(new List<string> {canceledException.Message});
        }
        catch(Exception exception)
        {
            var response = BaseResponse<string>.BadRequest(new List<string> {exception.Message});
            return response;
        }
    }

    public async Task<BaseResponse<BaseUser>> Register(RegistrationData userInfo, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            BaseResponse<BaseUser> result;

            var username = userInfo.UserName.ToLower();

            if(await _authRepository.UserExists(username))
            {
                throw new Exception("User already exists");
            }

            var entity = await RegisterUser(userInfo);

            var baseUser = GenericMapper<User, BaseUser>.Map(entity);
            result = BaseResponse<BaseUser>.Ok(baseUser);

            return result;
        }
        catch(TaskCanceledException canceledException)
        {
            return BaseResponse<BaseUser>.BadRequest(new List<string> {canceledException.Message});
        }
        catch(Exception exception)
        {
            return BaseResponse<BaseUser>.BadRequest(new List<string> {exception.Message});
            
        }
    }

    private async Task<User> RegisterUser(RegistrationData userInfo)
    {
        var entity = new User
        {
            Id = Guid.NewGuid(),
            Username = userInfo.UserName,
            Email = userInfo.Email,
            PhoneNumber = userInfo.PhoneNumber
        };

        await _authRepository.Register(entity, userInfo.Password);
        await _authRepository.SaveChanges();
        return entity;
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