namespace Cards.Domain.Entities;

public sealed class CheckList : BaseEntity<Guid>
{
    public Guid CardId { get; set; }
    public required string Title { get; set; }

    // Navigation properties
    public ICollection<CheckListItem> Items { get; } = new List<CheckListItem>();
    public Card? Card { get; set; }
}
