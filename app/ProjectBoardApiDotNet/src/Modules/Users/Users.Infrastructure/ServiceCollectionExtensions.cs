using System;
using Common.Application.Abstractions;
using Common.Constants;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Persistence;

namespace Users.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<UserDbContext>(
            (serviceProvider, options) =>
            {
                var interceptor =
                    serviceProvider.GetRequiredService<DomainEventDispatcherInterceptor>();

                options
                    .UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        npgsqlOptions =>
                            npgsqlOptions.MigrationsHistoryTable(
                                HistoryRepository.DefaultTableName,
                                DatabaseSchema.User
                            )
                    )
                    .AddInterceptors(interceptor)
                    .EnableServiceProviderCaching()
                    .EnableServiceProviderCaching(false);
            }
        );

        services.AddScoped<IUsersUnitOfWork>(provider =>
            provider.GetRequiredService<UserDbContext>()
        );

        return services;
    }
}
