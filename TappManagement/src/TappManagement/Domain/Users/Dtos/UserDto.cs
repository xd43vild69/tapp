namespace TappManagement.Domain.Users.Dtos;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public string IGUser { get; set; }
    public Guid? AppointmentId { get; set; }
}
