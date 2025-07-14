using System.Threading;
using SharedKernel.Entities;

namespace Boards.Application.Abstractions;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Board> AddAsync(Board entity, CancellationToken cancellationToken = default);
}
