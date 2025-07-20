using Cards.Infrastructure.Persistence.Configurations;
using SharedKernel.Constants;

namespace Cards.Infrastructure.Persistence;

public class CardDbContext(DbContextOptions<CardDbContext> options) : DbContext(options)
{
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardLabel> CardLabels { get; set; }
    public DbSet<CheckList> CheckLists { get; set; }
    public DbSet<CheckListItem> CheckListItems { get; set; }
    public DbSet<Comment> Comments { get; set; }

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
