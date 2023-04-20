namespace TappManagement.Domain.Admins.DomainEvents;

public sealed class AdminUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            