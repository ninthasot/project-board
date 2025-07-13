using Microsoft.AspNetCore.Identity;

namespace Identity.Entities;

public class ApplicationUser : IdentityUser
{
    public required string DisplayName { get; set; }
}
