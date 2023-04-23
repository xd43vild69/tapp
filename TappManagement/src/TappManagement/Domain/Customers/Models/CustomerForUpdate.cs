namespace TappManagement.Domain.Customers.Models;

public sealed class CustomerForUpdate
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsSync { get; set; }
    public string Cellphone { get; set; }
    public string IGUser { get; set; }
    public Guid? AppointmentId { get; set; }
}
