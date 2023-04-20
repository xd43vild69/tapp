namespace TappManagement.Controllers.v1;

using TappManagement.Domain.Admins.Features;
using TappManagement.Domain.Admins.Dtos;
using TappManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/admins")]
[ApiVersion("1.0")]
public sealed class AdminsController: ControllerBase
{
    private readonly IMediator _mediator;


    // dev branch
    public AdminsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Gets a list of all Admins.
    /// </summary>
    [HttpGet(Name = "GetAdmins")]
    public async Task<IActionResult> GetAdmins([FromQuery] AdminParametersDto adminParametersDto)
    {
        var query = new GetAdminList.Query(adminParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a single Admin by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetAdmin")]
    public async Task<ActionResult<AdminDto>> GetAdmin(Guid id)
    {
        var query = new GetAdmin.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Creates a new Admin record.
    /// </summary>
    [HttpPost(Name = "AddAdmin")]
    public async Task<ActionResult<AdminDto>> AddAdmin([FromBody]AdminForCreationDto adminForCreation)
    {
        var command = new AddAdmin.Command(adminForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetAdmin",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Updates an entire existing Admin.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateAdmin")]
    public async Task<IActionResult> UpdateAdmin(Guid id, AdminForUpdateDto admin)
    {
        var command = new UpdateAdmin.Command(id, admin);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Admin record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteAdmin")]
    public async Task<ActionResult> DeleteAdmin(Guid id)
    {
        var command = new DeleteAdmin.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
