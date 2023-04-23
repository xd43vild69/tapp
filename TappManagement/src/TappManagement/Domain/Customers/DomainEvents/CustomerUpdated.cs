namespace TappManagement.Domain.Customers.DomainEvents;

public sealed class CustomerUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            