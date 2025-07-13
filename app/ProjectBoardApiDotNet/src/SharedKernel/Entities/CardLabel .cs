namespace SharedKernel.Entities;

public class CardLabel : BaseEntity<Guid>
{
    public Guid CardId { get; set; }
    public Guid LabelId { get; set; }

    // Navigation properties
    public Card? Card { get; set; }
    public Label? Label { get; set; }
}
