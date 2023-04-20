namespace TappManagement.Domain.Appointments.Models;

public sealed class AppointmentForUpdate
{
    public int Size { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Reference { get; set; }
    public string Payment { get; set; }
    public bool Approve { get; set; }
    public Guid UserId { get; set; }
}
