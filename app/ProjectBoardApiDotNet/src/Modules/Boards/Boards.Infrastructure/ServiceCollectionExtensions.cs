using Boards.Domain.Abstractions;
using Boards.Infrastructure.Persistence;
using Boards.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Constants;

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
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.Board
                    )
            );
        });

        services.AddScoped<IBoardRepository, BoardRepository>();

        return services;
    }
}
