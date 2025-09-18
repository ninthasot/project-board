using Common.Application.Abstractions;
using Common.Constants;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Persistence;

namespace Users.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UserCardServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<UserDbContext>(options =>
        {
            options
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(
                            HistoryRepository.DefaultTableName,
                            DatabaseSchema.User
                        )
                )
                .EnableServiceProviderCaching()
                .EnableServiceProviderCaching(false);
        });

        services.AddScoped<IUsersUnitOfWork>(provider =>
            provider.GetRequiredService<UserDbContext>()
        );

        return services;
    }
}
