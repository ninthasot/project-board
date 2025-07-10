namespace Domain.Entities;

public class Label : BaseAuditableEntity<Guid>
{
    public Guid BoardId { get; set; }
    public required string Name { get; set; }
    public required string HexColor { get; set; }

    // Navigation properties
    public Board? Board { get; set; }
    public ICollection<CardLabel> CardLabels { get; } = new List<CardLabel>();
}
