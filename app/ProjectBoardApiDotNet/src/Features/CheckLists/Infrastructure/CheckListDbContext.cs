using SharedKernel.Constants;

namespace CheckLists.Infrastructure;

public class CheckListDbContext(DbContextOptions<CheckListDbContext> options) : DbContext(options)
{
    public DbSet<CheckList> CheckLists { get; set; }
    public DbSet<CheckListItem> CheckListItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.CheckList);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckListDbContext).Assembly);
    }
}
