namespace SharedKernel.Entities;

public class BaseEntity<TId> : IEntity<TId>
{
    public required TId Id { get; set; }
}
