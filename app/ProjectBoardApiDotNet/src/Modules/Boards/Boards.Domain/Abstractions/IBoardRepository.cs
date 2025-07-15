using Boards.Domain.Entities;

namespace Boards.Domain.Abstractions;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Board> AddAsync(Board entity, CancellationToken cancellationToken = default);
}
