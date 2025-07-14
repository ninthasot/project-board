namespace SharedKernel.Entities;

public interface IAuditableEntity<TId> : IEntity<TId>
{
    DateTimeOffset CreatedAtUtc { get; set; }
    DateTimeOffset? UpdatedAtUtc { get; set; }
    Guid CreatedBy { get; set; }
    Guid? UpdatedBy { get; set; }
}
