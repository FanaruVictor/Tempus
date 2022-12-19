using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Commands.Categories.Create;
using Tempus.Core.Commands.Categories.Delete;
using Tempus.Core.Commands.Categories.Update;
using Tempus.Core.Models.Category;
using Tempus.Core.Queries.Categories.GetAll;
using Tempus.Core.Queries.Categories.GetById;

namespace Tempus.API.Controllers;

/// <summary>
/// CategoryController is responsible with requests designed for categories
/// </summary>
public class CategoriesController : BaseController
{
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    public CategoriesController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Get all categories. If UserId is specified. This action will return all categories created by the specified user,
    /// otherwise it will return all categories from database 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<BaseCategory>>> GetAll([FromQuery] GetAllCategoriesQuery query)
        => HandleResponse(await _mediator.Send(query));

    /// <summary>
    /// For a specified Id a category will be returned if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseCategory>> GetById([FromRoute] Guid id)
        => HandleResponse(await _mediator.Send(new GetCategoryByIdQuery{Id = id}));

    /// <summary>
    /// Create a category and saves it into database
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BaseCategory>> Create([FromBody] CreateCategoryCommand command)
        => HandleResponse(await _mediator.Send(command));

    /// <summary>
    /// Updates a category proprieties
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<BaseCategory>> Update([FromBody] UpdateCategoryCommand command)
        => HandleResponse(await _mediator.Send(command));
    
    /// <summary>
    /// For a specified Id a category will be deleted from database if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id)
        => HandleResponse(await _mediator.Send(new DeleteCategoryCommand
        {
            Id = id
        }));
}