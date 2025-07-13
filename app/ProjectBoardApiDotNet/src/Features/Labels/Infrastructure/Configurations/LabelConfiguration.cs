using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labels.Infrastructure.Configurations;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(l => l.Id);

        builder.Property(l => l.BoardId).IsRequired();

        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);

        builder.Property(l => l.HexColor).IsRequired().HasMaxLength(7);

        // Indexes for fast lookup
        builder.HasIndex(l => l.BoardId);
        builder.HasIndex(l => l.Name);
    }
}
