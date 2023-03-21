using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Models.Photo;
using Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;
using Tempus.Infrastructure.Commands.ProfilePhoto.DeleteProfilePhoto;
using Tempus.Infrastructure.Commands.ProfilePhoto.UpdateProfilePhoto;
using Tempus.Infrastructure.Queries.ProfilePhoto.ProfilePhotoGetById;

namespace Tempus.API.Controllers;

[ApiVersion("1.0")]
public class ProfilePhotoController : BaseController
{
    public ProfilePhotoController(IMediator mediator) : base(mediator) { }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get([FromRoute] ProfilePhotoGetByIdQuery query)
    {
        return HandleResponse(await _mediator.Send(query));
    }
    
    
    [HttpPost]
    public async Task<ActionResult<PhotoDetails>> Add([FromForm] AddProfilePhotoCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
    
    [HttpPut]
    public async Task<ActionResult<PhotoDetails>> Update([FromForm] UpdateProfilePhotoCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete([FromBody] DeleteProfilePhotoCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
}