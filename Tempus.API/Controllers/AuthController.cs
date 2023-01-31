using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tempus.Infrastructure.Commands.Auth.Login;
using Tempus.Infrastructure.Commands.Auth.Register;
using Tempus.Infrastructure.Models.User;

namespace Tempus.API.Controllers;

[AllowAnonymous]
[ApiVersion("1.0")]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<BaseUser>> Register(RegisterUserCommand registerUserCommand) =>
        HandleResponse(await _mediator.Send(registerUserCommand));

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginComand loginComand) =>
        HandleResponse(await _mediator.Send(loginComand));
}