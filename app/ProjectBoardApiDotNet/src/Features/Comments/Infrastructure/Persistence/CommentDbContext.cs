using Comments.Infrastructure.Persistence.Configurations;
using SharedKernel.Constants;

namespace Comments.Infrastructure.Persistence;

public class CommentDbContext(DbContextOptions<CommentDbContext> options) : DbContext(options)
{
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Comment);

        modelBuilder.ApplyConfiguration(new CommentConfiguration());
    }
}
