using Common.Application.Abstractions;
using Common.Constants;
using Common.Infrastructure.Persistence.Interceptors;
using Labels.Domain.Abstractions;
using Labels.Infrastructure.Persistence;
using Labels.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Labels.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLabelServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<LabelDbContext>(
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
                                DatabaseSchema.Label
                            )
                    )
                    .AddInterceptors(interceptor)
                    .EnableServiceProviderCaching()
                    .EnableSensitiveDataLogging(false);
            }
        );

        services.AddScoped<ILabelsUnitOfWork>(provider =>
            provider.GetRequiredService<LabelDbContext>()
        );

        services.AddScoped<ILabelRepository, LabelRepository>();

        return services;
    }
}
