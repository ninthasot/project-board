namespace SharedKernel.Entities;

public class User : BaseEntity<string>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public required string DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; } = true;
}
