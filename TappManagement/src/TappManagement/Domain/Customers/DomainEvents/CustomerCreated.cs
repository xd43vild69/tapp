namespace TappManagement.Domain.Customers.DomainEvents;

public sealed class CustomerCreated : DomainEvent
{
    public Customer Customer { get; set; } 
}
            