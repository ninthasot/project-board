using Cards.Infrastructure.Persistence.Configurations;
using Common.Application.Abstractions;
using Common.Constants;

namespace Cards.Infrastructure.Persistence;

public sealed class CardDbContext : DbContext, ICardsUnitOfWork
{
    public CardDbContext(DbContextOptions<CardDbContext> options)
        : base(options) { }

    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardLabel> CardLabels => Set<CardLabel>();
    public DbSet<CheckList> CheckLists => Set<CheckList>();
    public DbSet<CheckListItem> CheckListItems => Set<CheckListItem>();
    public DbSet<Comment> Comments => Set<Comment>();

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

        modelBuilder.HasDefaultSchema(DatabaseSchema.Card);

        modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new CardLabelConfiguration());
        modelBuilder.ApplyConfiguration(new CheckListConfiguration());
        modelBuilder.ApplyConfiguration(new CheckListItemConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
    }
}
