using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attachments.Infrastructure.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName).IsRequired().HasMaxLength(255);

        builder.Property(a => a.FileType).IsRequired().HasMaxLength(100);

        builder.Property(a => a.Path).IsRequired().HasMaxLength(500);

        builder.Property(a => a.FileSize).IsRequired();

        builder.Property(a => a.UploadedAtUtc).IsRequired();

        builder.Property(a => a.UploadedBy).IsRequired();

        builder
            .HasOne(a => a.Card)
            .WithMany(c => c.Attachments)
            .HasForeignKey(a => a.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for fast lookup
        builder.HasIndex(a => a.CardId);
        builder.HasIndex(a => a.UploadedBy);
    }
}
