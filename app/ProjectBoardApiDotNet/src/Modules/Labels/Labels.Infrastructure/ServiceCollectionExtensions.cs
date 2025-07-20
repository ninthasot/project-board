using Common.Constants;
using Labels.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Labels.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection LabelCardServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<LabelDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.Label
                    )
            );
        });

        return services;
    }
}
