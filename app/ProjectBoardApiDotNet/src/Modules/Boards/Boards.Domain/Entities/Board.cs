using Boards.Domain.Events;

namespace Boards.Domain.Entities;

public sealed class Board : AggregateRoot<Guid>
{
    public required string Title { get; private set; }
    public required string Description { get; private set; }

    // Navigation properties
    public ICollection<Column> Columns { get; } = [];
    public ICollection<BoardMember> BoardMembers { get; } = [];

    private Board() { }

    public static Board Create(string title, string description, Guid createdBy)
    {
        var board = new Board
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            CreatedAtUtc = DateTimeOffset.UtcNow,
            UpdatedAtUtc = DateTimeOffset.UtcNow,
        };
        board.RaiseDomainEvent(new BoardCreatedEvent(board.Id, title, description, createdBy));
        return board;
    }

    public void UpdateDetails(string title, string description, Guid updatedBy)
    {
        Title = title;
        Description = description;
        UpdatedAtUtc = DateTimeOffset.UtcNow;
        RaiseDomainEvent(new BoardUpdatedEvent(Id, title, description, updatedBy));
    }

    public Column AddColumn(string title, int position, Guid createdBy)
    {
        var column = new Column
        {
            Id = Guid.NewGuid(),
            BoardId = Id,
            Title = title,
            Position = position,
            CreatedAtUtc = DateTimeOffset.UtcNow,
            UpdatedAtUtc = DateTimeOffset.UtcNow,
        };
        Columns.Add(column);
        RaiseDomainEvent(new ColumnAddedToBoardEvent(Id, column.Id, title, position));
        return column;
    }

    public void AddMember(Guid userId, int role, Guid addedBy)
    {
        var existingMember = BoardMembers.FirstOrDefault(m => m.UserId == userId);
        if (existingMember != null)
            return;

        var boardMember = new BoardMember
        {
            Id = Guid.NewGuid(),
            BoardId = Id,
            UserId = userId,
            Role = role,
            JoinedAtUtc = DateTimeOffset.UtcNow,
        };
        BoardMembers.Add(boardMember);
        RaiseDomainEvent(new BoardMemberAddedEvent(Id, userId, role));
    }
}
