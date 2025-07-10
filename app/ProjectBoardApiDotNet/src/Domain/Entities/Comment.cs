namespace Domain.Entities;

public class Comment : BaseAuditableEntity<Guid>
{
    public Guid CardId { get; set; }
    public Guid UserId { get; set; }
    public required string Content { get; set; }

    // Navigation property only for Card
    public Card? Card { get; set; }
    // Removed User navigation property (projection only)
}
