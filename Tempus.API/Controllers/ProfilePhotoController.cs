using System.Reflection.Metadata.Ecma335;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Photo;
using Tempus.Infrastructure.Queries.ProfilePhoto.ProfilePhotoGetById;

namespace Tempus.API.Controllers;

[ApiVersion("1.0")]
public class ProfilePhotoController : BaseController
{

    public ProfilePhotoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> Get([FromRoute] ProfilePhotoGetByIdQuery query) => HandleResponse(await _mediator.Send(query));

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<PhotoDetails>> Add([FromBody] AddProfilePhotoCommand command) =>
        HandleResponse(await _mediator.Send(command));

}