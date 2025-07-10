namespace Domain.Entities;

public class User : BaseEntity<string>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public required string DisplayName { get; set; }
    // Removed navigation properties (projection only)
}
