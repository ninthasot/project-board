namespace SharedKernel.Entities;

public sealed class Card : BaseAuditableEntity<Guid>
{
    public Guid ColumnId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Position { get; set; }

    // Navigation properties
    public ICollection<CardLabel> CardLabels { get; } = new List<CardLabel>();
}
