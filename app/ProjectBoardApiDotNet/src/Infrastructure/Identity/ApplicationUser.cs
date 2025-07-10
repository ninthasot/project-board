using Domain.Entities;
using Domain.Errors.Factories;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    // You can add more properties as needed

    // Mapping from Domain.Entities.User
    public static Result<ApplicationUser> FromDomain(User user)
    {
        try
        {
            if (user is null)
                return Result.Fail<ApplicationUser>("User cannot be null");
            return Result.Ok(new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName
            });
        }
        catch (Exception ex)
        {
            return Result.Fail(SharedErrorFactory.ExceptionalError(ex));
        }
    }

    // Mapping to Domain.Entities.User
    public Result<User> ToDomain()
    {
        try
        {
            var domainUser = new User
            {
                Id = this.Id,
                UserName = this.UserName,
                Email = this.Email,
                DisplayName = this.DisplayName
            };
            return Result.Ok(domainUser);
        }
        catch (Exception ex)
        {
            return Result.Fail(SharedErrorFactory.ExceptionalError(ex));
        }
    }
}