using Cards.Infrastructure.Persistence.Configurations;
using SharedKernel.Constants;

namespace Cards.Infrastructure.Persistence;

public class CardDbContext(DbContextOptions<CardDbContext> options) : DbContext(options)
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardLabel> CardLabels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Card);

        modelBuilder.ApplyConfiguration(new CardConfiguration());

        modelBuilder.ApplyConfiguration(new CardLabelConfiguration());
    }
}
