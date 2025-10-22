using Labels.Domain.Abstractions;

namespace Labels.Infrastructure.Persistence.Repositories;

internal sealed class LabelRepository : ILabelRepository
{
    private readonly LabelDbContext _context;

    public LabelRepository(LabelDbContext context)
    {
        _context = context;
    }

    public async Task<Label?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Labels.AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<List<Label>> GetByBoardIdAsync(
        Guid boardId,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Labels.AsNoTracking()
            .Where(l => l.BoardId == boardId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Label> AddAsync(Label entity, CancellationToken cancellationToken = default)
    {
        await _context.Labels.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task AddRangeAsync(
        IEnumerable<Label> entities,
        CancellationToken cancellationToken = default
    )
    {
        await _context.Labels.AddRangeAsync(entities, cancellationToken);
    }
}
