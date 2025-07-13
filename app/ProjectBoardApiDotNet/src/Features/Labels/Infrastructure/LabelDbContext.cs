using SharedKernel.Constants;

namespace Labels.Infrastructure;

public class LabelDbContext(DbContextOptions<LabelDbContext> options) : DbContext(options)
{
    public DbSet<Label> Labels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Label);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabelDbContext).Assembly);
    }
}
