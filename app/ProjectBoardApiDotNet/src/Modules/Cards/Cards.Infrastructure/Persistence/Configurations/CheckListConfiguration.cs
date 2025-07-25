namespace Cards.Infrastructure.Persistence.Configurations;

public class CheckListConfiguration : IEntityTypeConfiguration<CheckList>
{
    public void Configure(EntityTypeBuilder<CheckList> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CardId).IsRequired();

        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);

        builder
            .HasMany(c => c.Items)
            .WithOne(i => i.CheckList)
            .HasForeignKey(i => i.CheckListId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(cl => cl.Card)
            .WithMany(c => c.CheckLists)
            .HasForeignKey(cl => cl.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for fast lookup
        builder.HasIndex(c => c.CardId);

        builder.HasIndex(c => c.Title);
    }
}
