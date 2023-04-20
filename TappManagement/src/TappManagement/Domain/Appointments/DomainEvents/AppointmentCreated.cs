namespace TappManagement.Domain.Appointments.DomainEvents;

public sealed class AppointmentCreated : DomainEvent
{
    public Appointment Appointment { get; set; } 
}
            