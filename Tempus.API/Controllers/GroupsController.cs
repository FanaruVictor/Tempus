using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Models.Group;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commands.Groups.Create;
using Tempus.Infrastructure.Commands.Groups.Delete;
using Tempus.Infrastructure.Commands.Groups.Update;
using Tempus.Infrastructure.Queries.Group.GetAllGroupsQuery;
using Tempus.Infrastructure.Queries.Group.GetGroupByIdQuery;
using Tempus.Infrastructure.Queries.Registrations.GetAll;

namespace Tempus.API.Controllers;

public class GroupsController : BaseController
{
    // GET
    public GroupsController(IMediator mediator) : base(mediator)
    {
    }

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

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id)
    {
        return HandleResponse(await _mediator.Send(new DeleteGroupCommand() { Id = id }));
    }

    [HttpGet("{groupId:Guid}/registrations")]
    public async Task<ActionResult<List<RegistrationOverview>>> GetAllRegistrations([FromRoute] Guid groupId)
    {
        return HandleResponse(await _mediator.Send(new GetAllRegistrationsQuery { GroupId = groupId }));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<GroupDetails>> GetById([FromRoute] Guid id)
    {
        return HandleResponse(await _mediator.Send(new GetGroupByIdQuery {Id = id}));
    }
    
    [HttpPut]
    public async Task<ActionResult<GroupOverview>> Update([FromForm] UpdateGroupCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
}