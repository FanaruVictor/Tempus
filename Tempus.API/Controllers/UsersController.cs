using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commands.Users.Delete;
using Tempus.Infrastructure.Commands.Users.Update;
using Tempus.Infrastructure.Queries.Users.GetAll;
using Tempus.Infrastructure.Queries.Users.GetById;

namespace Tempus.API.Controllers;

/// <summary>
/// UsersController is responsible for requests designed for users
/// </summary>
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
    ///     For a specified Id a user will be returned if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BaseUser>> GetById([FromRoute] Guid id) => HandleResponse(await _mediator.Send(new GetUserByIdQuery { Id = id }));

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
    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id) =>
        HandleResponse(await _mediator.Send(new DeleteUserCommand
        {
            Id = id
        }));
}