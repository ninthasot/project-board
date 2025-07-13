using SharedKernel.Constants;

namespace Comments.Infrastructure;

public class CommentDbContext(DbContextOptions<CommentDbContext> options) : DbContext(options)
{
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Comment);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentDbContext).Assembly);
    }
}
