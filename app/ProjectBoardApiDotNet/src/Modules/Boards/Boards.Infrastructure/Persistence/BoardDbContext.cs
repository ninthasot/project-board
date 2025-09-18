using Boards.Infrastructure.Persistence.Configurations;
using Common.Application.Abstractions;
using Common.Constants;

namespace Boards.Infrastructure.Persistence;

public sealed class BoardDbContext : DbContext, IBoardsUnitOfWork
{
    public BoardDbContext(DbContextOptions<BoardDbContext> options)
        : base(options) { }

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<BoardMember> BoardMembers => Set<BoardMember>();
    public DbSet<Column> Columns => Set<Column>();

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

        modelBuilder.HasDefaultSchema(DatabaseSchema.Board);

        modelBuilder.ApplyConfiguration(new BoardConfiguration());
        modelBuilder.ApplyConfiguration(new BoardMemberConfiguration());
        modelBuilder.ApplyConfiguration(new ColumnConfiguration());
    }
}
