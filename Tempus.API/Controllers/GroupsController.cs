using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Models.Group;
using Tempus.Infrastructure.Commands.Groups.Create;
using Tempus.Infrastructure.Queries.Group;

namespace Tempus.API.Controllers;

public class GroupsController : BaseController
{
    // GET
    public GroupsController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<ActionResult<bool>> Add([FromForm] CreateGroupCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GroupOverview>>> GetAll()
    {
        return HandleResponse(await _mediator.Send(new GetAllGroupsQuery()));
    }
}