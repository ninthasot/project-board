namespace SharedKernel.Entities;

public sealed class CheckList : BaseEntity<Guid>
{
    public Guid CardId { get; set; }
    public required string Title { get; set; }

    // Navigation properties
    public Card? Card { get; set; }
    public ICollection<CheckListItem> Items { get; } = new List<CheckListItem>();
}
