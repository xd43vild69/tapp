namespace TappManagement.Domain.Admins.Dtos;

public sealed class AdminDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsSync { get; set; }
    public Guid UserId { get; set; }
}
