using Labels.Domain.Entities;

namespace Labels.Domain.Abstractions;

public interface ILabelRepository
{
    Task<Label?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Label>> GetByBoardIdAsync(
        Guid boardId,
        CancellationToken cancellationToken = default
    );
    Task<Label> AddAsync(Label entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Label> entities, CancellationToken cancellationToken = default);
}
