namespace TappManagement.Controllers.v1;

using TappManagement.Domain.Appointments.Features;
using TappManagement.Domain.Appointments.Dtos;
using TappManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/appointments")]
[ApiVersion("1.0")]
public sealed class AppointmentsController: ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Gets a list of all Appointments.
    /// </summary>
    [HttpGet(Name = "GetAppointments")]
    public async Task<IActionResult> GetAppointments([FromQuery] AppointmentParametersDto appointmentParametersDto)
    {
        var query = new GetAppointmentList.Query(appointmentParametersDto);
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
    /// Gets a single Appointment by ID.
    /// </summary>
    [HttpGet("{id:guid}", Name = "GetAppointment")]
    public async Task<ActionResult<AppointmentDto>> GetAppointment(Guid id)
    {
        var query = new GetAppointment.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Creates a new Appointment record.
    /// </summary>
    [HttpPost(Name = "AddAppointment")]
    public async Task<ActionResult<AppointmentDto>> AddAppointment([FromBody]AppointmentForCreationDto appointmentForCreation)
    {
        var command = new AddAppointment.Command(appointmentForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetAppointment",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Updates an entire existing Appointment.
    /// </summary>
    [HttpPut("{id:guid}", Name = "UpdateAppointment")]
    public async Task<IActionResult> UpdateAppointment(Guid id, AppointmentForUpdateDto appointment)
    {
        var command = new UpdateAppointment.Command(id, appointment);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Appointment record.
    /// </summary>
    [HttpDelete("{id:guid}", Name = "DeleteAppointment")]
    public async Task<ActionResult> DeleteAppointment(Guid id)
    {
        var command = new DeleteAppointment.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
