using Common.Application.Abstractions;
using Common.Constants;
using Labels.Domain.Entities;
using Labels.Infrastructure.Persistence.Configurations;

namespace Labels.Infrastructure.Persistence;

public sealed class LabelDbContext : DbContext, ILabelsUnitOfWork
{
    public LabelDbContext(DbContextOptions<LabelDbContext> options)
        : base(options) { }

    public DbSet<Label> Labels => Set<Label>();

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

        modelBuilder.HasDefaultSchema(DatabaseSchema.Label);

        modelBuilder.ApplyConfiguration(new LabelConfiguration());
    }
}
