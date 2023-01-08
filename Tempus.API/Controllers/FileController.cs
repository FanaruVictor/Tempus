using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Queries.Registrations.Download;

namespace Tempus.API.Controllers;

[ApiVersion("1.0")]
public class FilesController : BaseController
{
    private readonly IRegistrationRepository _registrationRepository;

    public FilesController(IMediator mediator, IRegistrationRepository registrationRepository) : base(mediator)
    {
        _registrationRepository = registrationRepository;
    }
    
    [HttpGet("download/{id}")] 
    public async Task<ActionResult<byte[]>> Download([FromRoute]Guid id) => 
    HandleResponse(await _mediator.Send(new DownloadQuery
    {
        Id = id
    }));
}