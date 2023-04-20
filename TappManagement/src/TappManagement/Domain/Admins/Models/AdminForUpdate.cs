namespace TappManagement.Domain.Admins.Models;

public sealed class AdminForUpdate
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsSync { get; set; }
    public Guid UserId { get; set; }
}
