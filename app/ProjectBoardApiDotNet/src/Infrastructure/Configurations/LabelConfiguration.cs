namespace Infrastructure.Configurations;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(l => l.Id);

        builder.Property(l => l.BoardId).IsRequired();

        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);

        builder.Property(l => l.HexColor).IsRequired().HasMaxLength(7);

        builder
            .HasOne(l => l.Board)
            .WithMany(b => b.Labels)
            .HasForeignKey(l => l.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(l => l.CardLabels)
            .WithOne(cl => cl.Label)
            .HasForeignKey(cl => cl.LabelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for fast lookup
        builder.HasIndex(l => l.BoardId);
        builder.HasIndex(l => l.Name);
    }
}
