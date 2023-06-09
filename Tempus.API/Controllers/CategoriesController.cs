﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commands.UserCategory.Create;
using Tempus.Infrastructure.Commands.UserCategory.Delete;
using Tempus.Infrastructure.Commands.UserCategory.Update;
using Tempus.Infrastructure.Queries.Categories.GetAll;
using Tempus.Infrastructure.Queries.Categories.GetById;

namespace Tempus.API.Controllers;

/// <summary>
///     CategoryController is responsible with requests designed for categories
/// </summary>
[ApiVersion("1.0")]
public class CategoriesController : BaseController
{
    /// <summary>
    ///     constructor
    /// </summary>
    /// <param name="mediator"></param>
    public CategoriesController(IMediator mediator) : base(mediator) { }

    /// <summary>
    ///     Get all categories. If UserId is specified. This action will return all categories created by the specified user,
    ///     otherwise it will return all categories from database
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<BaseCategory>>> GetAll()
    {
        return HandleResponse(await _mediator.Send(new GetAllCategoriesQuery()));
    }
    
    [HttpGet("groups/{groupId}")]
    public async Task<ActionResult<List<BaseCategory>>> GetAll([FromRoute] Guid groupId)
    {
        return HandleResponse(await _mediator.Send(new GetAllCategoriesQuery {GroupId = groupId}));
    }

    /// <summary>
    ///     For a specified Id a category will be returned if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseCategory>> GetById([FromRoute] Guid id)
    {
        return HandleResponse(await _mediator.Send(new GetCategoryByIdQuery {Id = id}));
    }

    /// <summary>
    ///     Create a category and saves it into database
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BaseCategory>> Create([FromBody] CreateCategoryCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }
    
    [HttpPost("groups/{groupId}")]
    public async Task<ActionResult<BaseCategory>> Create([FromRoute] Guid groupId, [FromBody] CreateCategoryCommand command)
    {
        command.GroupId = groupId;
        return HandleResponse(await _mediator.Send(command));
    }

    /// <summary>
    ///     Updates a category proprieties
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<BaseCategory>> Update([FromBody] UpdateCategoryCommand command)
    {
        return HandleResponse(await _mediator.Send(command));
    }

    /// <summary>
    ///     For a specified Id a category will be deleted from database if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete([FromRoute] Guid id)
    {
        return HandleResponse(await _mediator.Send(new DeleteCategoryCommand
        {
            Id = id
        }));
    }
}