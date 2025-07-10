// This file has been moved to Infrastructure/Identity/User.cs to support IdentityUser inheritance.

namespace Domain.Entities;

public class User
{
    public string Id { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    // Add domain-specific properties and methods here
}