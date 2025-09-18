namespace Common.Domain.Entities;

public interface IAuditableEntity<TId> : IEntity<TId>
{
    DateTimeOffset CreatedAtUtc { get; set; }
    DateTimeOffset? UpdatedAtUtc { get; set; }
    string CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
}