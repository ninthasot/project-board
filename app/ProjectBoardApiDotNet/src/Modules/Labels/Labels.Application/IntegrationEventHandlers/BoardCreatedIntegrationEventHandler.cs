using Common.Application.Abstractions;
using Common.Application.Events;
using Common.Contracts.Events.Boards;
using Labels.Application.Defaults;
using Labels.Domain.Abstractions;
using Labels.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        ArgumentNullException.ThrowIfNull(notification);

        ArgumentNullException.ThrowIfNull(notification.Event);

        var evt = notification.Event;

        _logger.LogInformation(
            "Labels module received BoardCreatedIntegrationEvent for Board {BoardId}. Creating default labels.",
            evt.BoardId
        );

        try
        {
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
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (DbUpdateException ex) when (IsDuplicateKeyViolation(ex))
        {
            _logger.LogInformation(
                ex,
                "Default labels for Board {BoardId} were already created by another handler instance. Idempotency achieved via database constraint.",
                evt.BoardId
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to create default labels for Board {BoardId}. Board will exist without default labels.",
                evt.BoardId
            );
        }
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

        // Map templates to domain entities
        return DefaultLabelDefinitions
            .Templates.Select(template => new Label
            {
                Id = Guid.NewGuid(),
                BoardId = notification.BoardId,
                Name = template.Name,
                HexColor = template.HexColor,
                CreatedAtUtc = now,
                CreatedBy = creatorId.ToString(),
            })
            .ToList();
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

    /// <summary>
    /// Determines if the DbUpdateException is due to a unique constraint violation (duplicate key).
    /// PostgreSQL error code 23505 indicates unique_violation.
    /// </summary>
    private static bool IsDuplicateKeyViolation(DbUpdateException ex)
    {
        // Check for PostgreSQL unique constraint violation
        var innerException = ex.InnerException?.Message ?? string.Empty;

        return innerException.Contains("23505", StringComparison.Ordinal) // PostgreSQL error code
            || innerException.Contains("duplicate key", StringComparison.OrdinalIgnoreCase)
            || innerException.Contains("unique constraint", StringComparison.OrdinalIgnoreCase)
            || innerException.Contains(
                "IX_Labels_BoardId_Name",
                StringComparison.OrdinalIgnoreCase
            );
    }
}
