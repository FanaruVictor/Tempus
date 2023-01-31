using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Infrastructure.Commands.Users.ChangeTheme;
using Tempus.Infrastructure.Commands.Users.Delete;
using Tempus.Infrastructure.Commands.Users.Update;
using Tempus.Infrastructure.Models.User;
using Tempus.Infrastructure.Queries.Users.GetAll;
using Tempus.Infrastructure.Queries.Users.GetById;
using Tempus.Infrastructure.Queries.Users.GetDetails;
using Tempus.Infrastructure.Queries.Users.GetTheme;

namespace Tempus.API.Controllers;

/// <summary>
/// UsersController is responsible for requests designed for users
/// </summary>
[ApiVersion("1.0")]
public class UsersController : BaseController
{
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    ///     Get all users from database
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<BaseUser>>> GetAll() => HandleResponse(await _mediator.Send(new GetAllUsersQuery()));

    /// <summary>
    /// Get current user details
    /// </summary>
    /// <returns></returns>
    [HttpGet("details")]
    public async Task<ActionResult<UserDetails>> GetDetails() =>
        HandleResponse(await _mediator.Send(new GetUserDetailsQuery()));

    /// <summary>
    ///     For a specified Id a user will be returned if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDetails>> GetById([FromRoute] Guid id) => HandleResponse(await _mediator.Send(new GetUserByIdQuery ()));

    /// <summary>
    ///     Update an user proprieties
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<BaseUser>> Update([FromBody] UpdateUserCommand command) => HandleResponse(await _mediator.Send(command));

    /// <summary>
    ///     For a specified Id a user will be deleted from database  if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id) =>
        HandleResponse(await _mediator.Send(new DeleteUserCommand
        {
            Id = id
        }));

    [HttpPut("changeTheme")]
    public async Task<ActionResult<UserDetails>> ChangeTheme([FromBody] ChangeThemeCommand command) =>
        HandleResponse(await _mediator.Send(command));

    [HttpGet("theme")]
    public async Task<ActionResult<bool>> GetTheme() => HandleResponse(await _mediator.Send(new GetThemeQuery()));

}