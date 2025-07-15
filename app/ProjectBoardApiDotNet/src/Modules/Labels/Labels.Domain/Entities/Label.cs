using SharedKernel.Entities;

namespace Labels.Domain.Entities;

public class Label : BaseAuditableEntity<Guid>
{
    public Guid BoardId { get; set; }
    public required string Name { get; set; }
    public required string HexColor { get; set; }
}
