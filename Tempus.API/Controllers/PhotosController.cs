using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Commons;
using Tempus.Core.Models;
using Tempus.Infrastructure.Commands.UserPhoto.Create;
using Tempus.Infrastructure.Commands.UserPhoto.Delete;
using Tempus.Infrastructure.Commands.UserPhoto.Update;
using Tempus.Infrastructure.Queries.UserPhoto.GetById;

namespace Tempus.API.Controllers;

[ApiVersion("1.0")]
public class PhotosController : BaseController
{
    public PhotosController(IMediator mediator) : base(mediator) { }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get([FromRoute] GetPhotoByIdQuery query)
    {
        return HandleResponse(await _mediator.Send(query));
    }
    
    
    [HttpPost]
    public async Task<ActionResult<PhotoDetails>> Add([FromForm] CreateUserPhotoCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
    
    [HttpPut]
    public async Task<ActionResult<PhotoDetails>> Update([FromForm] UpdateUserPhotoCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete([FromBody] DeleteUserPhotoCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
}