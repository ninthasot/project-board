namespace Boards.Infrastructure.Persistence.Configurations;

public class BoardMemberConfiguration : IEntityTypeConfiguration<BoardCreatedEvent>
{
    public void Configure(EntityTypeBuilder<BoardCreatedEvent> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(m => m.Id);

        builder.Property(m => m.BoardId).IsRequired();

        builder.Property(m => m.UserId).IsRequired();

        builder.Property(m => m.Role).IsRequired();

        builder.Property(m => m.JoinedAtUtc).IsRequired();

        builder
            .HasOne(m => m.Board)
            .WithMany(b => b.BoardMembers)
            .HasForeignKey(m => m.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes for fast lookup
        builder.HasIndex(m => m.BoardId);
        builder.HasIndex(m => m.UserId);
    }
}
