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
            typeof(AttachmentDbContext),
            typeof(BoardDbContext),
            typeof(CardDbContext),
            typeof(CheckListDbContext),
            typeof(CommentDbContext),
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
