using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tempus.Core.Entities;
using Tempus.Core.IServices;
using Tempus.Core.Models.Auth;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Services.AuthService;

namespace Tempus.API.Controllers;

[AllowAnonymous, ApiVersion("1.0")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IMediator mediator, IAuthService authService, IConfiguration configuration) : base(mediator)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDetails>> Register([FromBody] RegistrationData userInfo)
    {
        return HandleResponse(await _authService.Register(userInfo, new CancellationToken()));
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCredentials credentials)
    {
        return HandleResponse(await _authService.Login(credentials, new CancellationToken()));
    }

    [HttpPost("loginWithGoogle")]
    public async Task<ActionResult<LoginResult>> LoginWithGoogle([FromBody] GoogleResponse googleResponse)
    {
        return HandleResponse(await _authService.LoginWithGoogle(googleResponse.googleToken, new CancellationToken()));
    }
}

public class GoogleResponse
{
    public string googleToken { get; set; }
}