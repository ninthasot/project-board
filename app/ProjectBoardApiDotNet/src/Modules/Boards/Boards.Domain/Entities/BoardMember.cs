namespace Boards.Domain.Entities;

public sealed class BoardMember : BaseEntity<Guid>
{
    public Guid BoardId { get; set; }
    public Guid UserId { get; set; }
    public int Role { get; set; }
    public DateTimeOffset JoinedAtUtc { get; set; }

    // Navigation property only for Board
    public Board? Board { get; set; }
}
