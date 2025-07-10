using Domain.Entities;
using Domain.Errors.Factories;
using FluentResults;
using Infrastructure.Identity;

namespace Application.Mappers;

public static class UserMapper
{
    public static Result<ApplicationUser> ToApplicationUser(User user)
    {
        try
        {
            if (user is null)
                return Result.Fail<ApplicationUser>("User cannot be null");

            return Result.Ok(
                new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    DisplayName = user.DisplayName ?? string.Empty,
                }
            );
        }
        catch (Exception ex)
        {
            return Result.Fail(SharedErrorFactory.ExceptionalError(ex));
        }
    }

    public static Result<User> ToDomainUser(ApplicationUser appUser)
    {
        try
        {
            if (appUser is null)
                return Result.Fail<User>("ApplicationUser cannot be null");
            var domainUser = new User
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                DisplayName = appUser.DisplayName,
            };
            return Result.Ok(domainUser);
        }
        catch (Exception ex)
        {
            return Result.Fail(SharedErrorFactory.ExceptionalError(ex));
        }
    }
}
