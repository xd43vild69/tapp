namespace TappManagement.Domain.Appointments.Dtos;

public sealed class AppointmentForCreationDto
{
    public decimal Size { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Reference { get; set; }
    public string Payment { get; set; }
    public bool Approve { get; set; }
    public Guid UserId { get; set; }
}
