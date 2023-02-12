using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tempus.Infrastructure.Models.Auth;
using Tempus.Infrastructure.Models.User;
using Tempus.Infrastructure.Services.AuthService;

namespace Tempus.API.Controllers;

[AllowAnonymous, ApiVersion("1.0")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    public AuthController(IMediator mediator, IAuthService authService) : base(mediator)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<BaseUser>> Register([FromBody] RegistrationData userInfo)
    {
        return HandleResponse(await _authService.Register(userInfo, new CancellationToken()));
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginCredentials credentials)
    {
        return HandleResponse(await _authService.Login(credentials, new CancellationToken()));
    }
}