namespace TappManagement.Domain.Appointments.DomainEvents;

public sealed class AppointmentUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            