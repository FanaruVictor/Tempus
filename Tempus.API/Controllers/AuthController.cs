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
using Tempus.Core.IServices.Auth;
using Tempus.Core.Models.User;
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
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCredentials credentials)
    {
        return HandleResponse(await _authService.Login(credentials, new CancellationToken()));
    }
}

