namespace Domain.Entities.Base;

public class BaseEntity<TId> : IEntity<TId>
{
    public required TId Id { get; set; }
}
