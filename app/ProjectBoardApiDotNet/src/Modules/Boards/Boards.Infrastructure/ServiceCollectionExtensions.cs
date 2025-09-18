using Boards.Domain.Abstractions;
using Boards.Infrastructure.Persistence;
using Boards.Infrastructure.Persistence.Repositories;
using Common.Application.Abstractions;
using Common.Constants;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<BoardDbContext>(options =>
        {
            options
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(
                            HistoryRepository.DefaultTableName,
                            DatabaseSchema.Board
                        )
                )
                .EnableServiceProviderCaching()
                .EnableSensitiveDataLogging(false);
        });

        services.AddScoped<IBoardsUnitOfWork>(provider =>
            provider.GetRequiredService<BoardDbContext>()
        );

        services.AddScoped<IBoardRepository, BoardRepository>();

        return services;
    }
}
