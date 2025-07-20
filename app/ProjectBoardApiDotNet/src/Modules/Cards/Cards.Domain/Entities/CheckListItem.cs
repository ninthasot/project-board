namespace Cards.Domain.Entities;

public sealed class CheckListItem : BaseEntity<Guid>
{
    public Guid CheckListId { get; set; }
    public required string Content { get; set; }
    public bool IsCompleted { get; set; }

    // Navigation property
    public CheckList? CheckList { get; set; }
}
