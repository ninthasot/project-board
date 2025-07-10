namespace Infrastructure.Configurations;

public class CardLabelConfiguration : IEntityTypeConfiguration<CardLabel>
{
    public void Configure(EntityTypeBuilder<CardLabel> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(cl => cl.Id);

        builder.Property(cl => cl.CardId).IsRequired();

        builder.Property(cl => cl.LabelId).IsRequired();

        builder
            .HasOne(cl => cl.Card)
            .WithMany(c => c.CardLabels)
            .HasForeignKey(cl => cl.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(cl => cl.Label)
            .WithMany(l => l.CardLabels)
            .HasForeignKey(cl => cl.LabelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for fast lookup
        builder.HasIndex(cl => cl.CardId);
        builder.HasIndex(cl => cl.LabelId);
    }
}
