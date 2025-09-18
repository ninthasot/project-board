namespace Common.Domain.Entities;

public class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity<TId>
{
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? UpdatedBy { get; set; }
}