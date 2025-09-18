using Common.Application.Abstractions;
using Common.Constants;
using Users.Infrastructure.Persistence.Configurations;

namespace Users.Infrastructure.Persistence;

public sealed class UserDbContext : DbContext, IUsersUnitOfWork
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    public bool HasActiveTransaction => Database.CurrentTransaction is not null;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
            return;

        await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is null)
            return;

        try
        {
            await SaveChangesAsync(cancellationToken);
            await Database.CurrentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await Database.CurrentTransaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.RollbackAsync(cancellationToken);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.User);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
