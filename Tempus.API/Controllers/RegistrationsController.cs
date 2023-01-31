using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Infrastructure.Commands.Registrations.Create;
using Tempus.Infrastructure.Commands.Registrations.Delete;
using Tempus.Infrastructure.Commands.Registrations.Update;
using Tempus.Infrastructure.Models.Registrations;
using Tempus.Infrastructure.Queries.Registrations.GetAll;
using Tempus.Infrastructure.Queries.Registrations.GetById;
using Tempus.Infrastructure.Queries.Registrations.LastUpdated;

namespace Tempus.API.Controllers;

/// <summary>
/// RegistrationsConstructor is responsible with requests designed for registrations
/// </summary>
[ApiVersion("1.0")]
public class RegistrationsController : BaseController
{
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public RegistrationsController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    ///     Get all registration. If a CategoryId is specified this action will return all registrations created for the
    ///     specified category,
    ///     otherwise it will return all registrations from database
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<DetailedRegistration>>> GetAll([FromQuery] GetAllRegistrationsQuery query) => HandleResponse(await _mediator.Send(query));

    /// <summary>
    ///     For a specified Id a registration will be returned if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseRegistration>> GetById([FromRoute] Guid id) => HandleResponse(await _mediator.Send(new GetRegistrationByIdQuery { Id = id }));

    /// <summary>
    ///     Create a registration and saves it into database
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BaseRegistration>> Create([FromBody] CreateRegistrationCommand command) => HandleResponse(await _mediator.Send(command));

    /// <summary>
    ///     Updates a registration proprieties
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<BaseRegistration>> Update([FromBody] UpdateRegistrationCommand command) => HandleResponse(await _mediator.Send(command));

    /// <summary>
    ///     For a specified Id a registration will be deleted from database if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id) =>
        HandleResponse(await _mediator.Send(new DeleteRegistrationCommand
        {
            Id = id
        }));

    /// <summary>
    ///Get last registration updated
    /// </summary>
    /// <returns></returns>
    [HttpGet("lastUpdated")]
    public async Task<ActionResult<BaseRegistration>> GetLastUpdated() =>
        HandleResponse(await _mediator.Send(new GetLastUpdatedRegsitrationQuery()));
}