using System;
using Cards.Infrastructure.Persistence;
using Common.Application.Abstractions;
using Common.Constants;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCardServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<CardDbContext>(
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
                                DatabaseSchema.Card
                            )
                    )
                    .AddInterceptors(interceptor)
                    .EnableServiceProviderCaching()
                    .EnableSensitiveDataLogging(false);
            }
        );

        services.AddScoped<ICardsUnitOfWork>(provider =>
            provider.GetRequiredService<CardDbContext>()
        );

        return services;
    }
}
