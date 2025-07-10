namespace Domain.Entities.Base;

public interface IEntity<TId>
{
    TId Id { get; set; }
}
