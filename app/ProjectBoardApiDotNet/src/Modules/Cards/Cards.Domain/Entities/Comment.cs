namespace Cards.Domain.Entities;

public class Comment : BaseAuditableEntity<Guid>
{
    public Guid CardId { get; set; }
    public Guid UserId { get; set; }
    public required string Content { get; set; }

    // Navigation property
    public Card? Card { get; set; }
}
