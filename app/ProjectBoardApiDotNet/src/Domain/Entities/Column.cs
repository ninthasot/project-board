namespace Domain.Entities;

public sealed class Column : BaseAuditableEntity<Guid>
{
    public Guid BoardId { get; set; }
    public required string Title { get; set; }
    public int Position { get; set; }

    // Navigation properties
    public Board? Board { get; set; }
    public ICollection<Card> Cards { get; } = new List<Card>();
}
