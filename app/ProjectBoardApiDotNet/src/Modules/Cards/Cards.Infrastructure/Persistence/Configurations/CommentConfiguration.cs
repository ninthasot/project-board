namespace Cards.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(cm => cm.Id);

        builder.Property(cm => cm.CardId).IsRequired();

        builder.Property(cm => cm.UserId).IsRequired();

        builder.Property(cm => cm.Content).IsRequired().HasMaxLength(2000);

        builder
            .HasOne(cm => cm.Card)
            .WithMany(c => c.Comments)
            .HasForeignKey(cm => cm.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for fast lookup
        builder.HasIndex(cm => cm.CardId);

        builder.HasIndex(cm => cm.UserId);
    }
}
