namespace Cards.Infrastructure.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ColumnId).IsRequired();

        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);

        builder.Property(c => c.Description).IsRequired().HasMaxLength(1000);

        builder.Property(c => c.Position).IsRequired();

        builder
            .HasOne(c => c.Column)
            .WithMany(col => col.Cards)
            .HasForeignKey(c => c.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Comments)
            .WithOne(cm => cm.Card)
            .HasForeignKey(cm => cm.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.CardLabels)
            .WithOne(cl => cl.Card)
            .HasForeignKey(cl => cl.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Attachments)
            .WithOne(a => a.Card)
            .HasForeignKey(a => a.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for fast lookup
        builder.HasIndex(c => c.ColumnId);
        builder.HasIndex(c => c.Title);
    }
}
