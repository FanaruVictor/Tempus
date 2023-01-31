using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Infrastructure.Queries.Registrations.Download;

namespace Tempus.API.Controllers;

[ApiVersion("1.0")]
public class FilesController : BaseController
{
    public FilesController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet("download/{id}")] 
    public async Task<ActionResult<byte[]>> Download([FromRoute]Guid id) => 
    HandleResponse(await _mediator.Send(new DownloadQuery
    {
        Id = id
    }));
}