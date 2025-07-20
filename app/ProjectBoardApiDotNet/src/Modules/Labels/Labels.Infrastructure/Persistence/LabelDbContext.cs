using Common.Constants;
using Labels.Domain.Entities;
using Labels.Infrastructure.Persistence.Configurations;

namespace Labels.Infrastructure.Persistence;

public class LabelDbContext(DbContextOptions<LabelDbContext> options) : DbContext(options)
{
    public DbSet<Label> Labels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Label);

        modelBuilder.ApplyConfiguration(new LabelConfiguration());
    }
}
