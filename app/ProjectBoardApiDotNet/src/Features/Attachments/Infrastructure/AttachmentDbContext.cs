using SharedKernel.Constants;

namespace Attachments.Infrastructure;

public class AttachmentDbContext(DbContextOptions<AttachmentDbContext> options) : DbContext(options)
{
    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Attachment);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AttachmentDbContext).Assembly);
    }
}
