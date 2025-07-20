using Boards.Infrastructure.Persistence;
using Cards.Infrastructure.Persistence;
using Labels.Infrastructure.Persistence;
using Users.Infrastructure.Persistence;

namespace Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationAsync(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var dbContexts = new Type[]
        {
            typeof(CardDbContext),
            typeof(BoardDbContext),
            typeof(LabelDbContext),
            typeof(UserDbContext),
            typeof(ProjectIdentityDbContext),
        };

        foreach (var dbContextType in dbContexts)
        {
            var dbContext = services.GetService(dbContextType) as DbContext;
            if (dbContext != null)
            {
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
