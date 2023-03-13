using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Models.Photo;
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

    [HttpPost, DisableRequestSizeLimit]
    public async Task<ActionResult<PhotoDetails>> Add()
    {
        if(!Request.Form.Files.Any())
        {
            return BadRequest("No files found in the request");
        }

        return Ok();
    }
}