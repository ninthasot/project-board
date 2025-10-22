using Common.Application.Abstractions;
using Common.Application.Events;
using Common.Contracts.Events.Boards;
using Labels.Domain.Abstractions;
using Labels.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Labels.Application.IntegrationEventHandlers;

internal sealed class BoardCreatedIntegrationEventHandler
    : INotificationHandler<IntegrationEventNotification<BoardCreatedIntegrationEvent>>
{
    private readonly ILabelRepository _labelRepository;
    private readonly ILabelsUnitOfWork _unitOfWork;
    private readonly ILogger<BoardCreatedIntegrationEventHandler> _logger;

    public BoardCreatedIntegrationEventHandler(
        ILabelRepository labelRepository,
        ILabelsUnitOfWork unitOfWork,
        ILogger<BoardCreatedIntegrationEventHandler> logger
    )
    {
        _labelRepository = labelRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(
        IntegrationEventNotification<BoardCreatedIntegrationEvent> notification,
        CancellationToken cancellationToken
    )
    {
        var evt = notification.Event;

        _logger.LogInformation(
            "Labels module received BoardCreatedIntegrationEvent for Board {BoardId}. Creating default labels.",
            evt.BoardId
        );

        // ACL: Validate incoming data from external module
        if (evt.BoardId == Guid.Empty)
        {
            _logger.LogWarning("Received invalid BoardId. Skipping default label creation.");
            return;
        }

        // ACL: Check for idempotency - prevent duplicate label creation
        var existingLabels = await _labelRepository.GetByBoardIdAsync(
            evt.BoardId,
            cancellationToken
        );

        if (existingLabels.Count > 0)
        {
            _logger.LogWarning(
                "Board {BoardId} already has {Count} labels. Skipping default label creation.",
                evt.BoardId,
                existingLabels.Count
            );
            return;
        }

        // ACL: Transform integration event into Labels module's domain model
        var defaultLabels = CreateDefaultLabels(evt);

        await _labelRepository.AddRangeAsync(defaultLabels, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created {Count} default labels for Board {BoardId}: '{BoardTitle}'",
            defaultLabels.Count,
            evt.BoardId,
            evt.Title
        );
    }

    /// <summary>
    /// ACL: Maps external board creation event to Labels module's domain concepts.
    /// Only extracts relevant data (BoardId, CreatedBy) and applies Labels module's business rules.
    /// </summary>
    private static List<Label> CreateDefaultLabels(BoardCreatedIntegrationEvent notification)
    {
        var now = DateTimeOffset.UtcNow;

        // ACL: Convert CreatedBy string to Guid (handle different formats from Boards module)
        var creatorId = ParseCreatorId(notification.CreatedBy);

        // Labels module's business rule: Create these specific default labels
        return
        [
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Priority: High",
                HexColor = "#D32F2F", // Red
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Priority: Medium",
                HexColor = "#FF9800", // Orange
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Priority: Low",
                HexColor = "#4CAF50", // Green
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Bug",
                HexColor = "#F44336", // Bright Red
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Feature",
                HexColor = "#2196F3", // Blue
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Enhancement",
                HexColor = "#9C27B0", // Purple
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
            new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = "Documentation",
                HexColor = "#607D8B", // Blue Grey
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
                UpdatedBy = null,
            },
        ];
    }

    /// <summary>
    /// ACL: Safely parse CreatedBy from external module (could be string, GUID, email, etc.)
    /// Protects Labels module from changes in Boards module's user identification format.
    /// </summary>
    private static Guid ParseCreatorId(string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            return Guid.Empty;

        return Guid.TryParse(createdBy, out var guid) ? guid : Guid.Empty;
    }
}
