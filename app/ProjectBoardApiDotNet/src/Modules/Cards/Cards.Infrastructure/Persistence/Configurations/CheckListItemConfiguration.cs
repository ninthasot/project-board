namespace Cards.Infrastructure.Persistence.Configurations;

public class CheckListItemConfiguration : IEntityTypeConfiguration<CheckListItem>
{
    public void Configure(EntityTypeBuilder<CheckListItem> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);

        builder.Property(i => i.CheckListId).IsRequired();

        builder.Property(i => i.Content).IsRequired().HasMaxLength(500);

        builder.Property(i => i.IsCompleted).IsRequired();

        builder
            .HasOne(i => i.CheckList)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CheckListId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for fast lookup
        builder.HasIndex(i => i.CheckListId);

        builder.HasIndex(i => i.IsCompleted);
    }
}
