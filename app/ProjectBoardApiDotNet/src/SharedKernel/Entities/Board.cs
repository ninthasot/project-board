namespace SharedKernel.Entities;

public sealed class Board : BaseAuditableEntity<Guid>
{
    public required string Title { get; set; }
    public required string Description { get; set; }

    // Navigation properties
    public ICollection<Column> Columns { get; } = new List<Column>();
    public ICollection<BoardMember> BoardMembers { get; } = new List<BoardMember>();
}
