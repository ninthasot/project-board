namespace SharedKernel.Entities.Base;

public class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity<TId>
{
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
