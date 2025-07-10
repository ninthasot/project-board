namespace Domain.Entities;

public sealed class Card : BaseAuditableEntity<Guid>
{
    public Guid ColumnId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Position { get; set; }

    // Navigation properties
    public Column? Column { get; set; }
    public ICollection<Comment> Comments { get; } = new List<Comment>();
    public ICollection<CardLabel> CardLabels { get; } = new List<CardLabel>();
    public ICollection<Attachment> Attachments { get; } = new List<Attachment>();
}
