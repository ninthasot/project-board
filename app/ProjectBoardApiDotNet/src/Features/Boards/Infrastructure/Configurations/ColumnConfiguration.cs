namespace Boards.Infrastructure.Configurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(col => col.Id);

        builder.Property(col => col.BoardId).IsRequired();

        builder.Property(col => col.Title).IsRequired().HasMaxLength(200);

        builder.Property(col => col.Position).IsRequired();

        builder
            .HasOne(col => col.Board)
            .WithMany(b => b.Columns)
            .HasForeignKey(col => col.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for fast lookup
        builder.HasIndex(col => col.BoardId);
        builder.HasIndex(col => col.Title);
    }
}
