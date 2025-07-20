using Cards.Infrastructure.Persistence;
using Common.Constants;
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
        services.AddDbContext<CardDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.Card
                    )
            );
        });

        return services;
    }
}
