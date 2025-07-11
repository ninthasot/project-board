using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddDataBase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ProjectBoardDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.ProjectBoard
                    )
            );
        });

        builder.Services.AddDbContext<ProjectIdentityDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        DatabaseSchema.Identity
                    )
            );
        });
        return builder;
    }
}
