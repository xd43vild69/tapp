namespace TappManagement.Domain.Admins.Dtos;

public sealed class AdminForUpdateDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsSync { get; set; }
    public Guid UserId { get; set; }
}
