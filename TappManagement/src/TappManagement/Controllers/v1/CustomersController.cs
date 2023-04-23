namespace TappManagement.Controllers.v1;

using TappManagement.Domain.Customers.Features;
using TappManagement.Domain.Customers.Dtos;
using TappManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/customers")]
[ApiVersion("1.0")]
public sealed class CustomersController: ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Gets a list of all Customers.
    /// </summary>
    [HttpGet(Name = "GetCustomers")]
    public async Task<IActionResult> GetCustomers([FromQuery] CustomerParametersDto customerParametersDto)
    {
        var query = new GetCustomerList.Query(customerParametersDto);
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
    /// Gets a single Customer by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetCustomer")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
    {
        var query = new GetCustomer.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Creates a new Customer record.
    /// </summary>
    [HttpPost(Name = "AddCustomer")]
    public async Task<ActionResult<CustomerDto>> AddCustomer([FromBody]CustomerForCreationDto customerForCreation)
    {
        var command = new AddCustomer.Command(customerForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetCustomer",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Updates an entire existing Customer.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(Guid id, CustomerForUpdateDto customer)
    {
        var command = new UpdateCustomer.Command(id, customer);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Customer record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteCustomer")]
    public async Task<ActionResult> DeleteCustomer(Guid id)
    {
        var command = new DeleteCustomer.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
