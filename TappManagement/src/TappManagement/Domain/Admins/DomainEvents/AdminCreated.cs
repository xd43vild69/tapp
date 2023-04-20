namespace TappManagement.Domain.Admins.DomainEvents;

public sealed class AdminCreated : DomainEvent
{
    public Admin Admin { get; set; } 
}
            