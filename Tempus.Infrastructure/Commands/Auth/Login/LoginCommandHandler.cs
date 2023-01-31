using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commands.Auth.Login;

namespace Tempus.Infrastructure.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginComand, BaseResponse<string>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }
    
    public async Task<BaseResponse<string>> Handle(LoginComand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            BaseResponse<string> response;

            var user = await _authRepository.Login(request.UserName, request.Password);

            if (user == null)
            {
                response = BaseResponse<string>.Unauthorized();
                return response;
            }

            CreateToken(user, out var tokenHandler, out var token);
            
            response = BaseResponse<string>.Ok(tokenHandler.WriteToken(token));

            return response;
        }
        catch (Exception exception)
        {
            var response = BaseResponse<string>.BadRequest(new List<string>{exception.Message});
            return response;
        }
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