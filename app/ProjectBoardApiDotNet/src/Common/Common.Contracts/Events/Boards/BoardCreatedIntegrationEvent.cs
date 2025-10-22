namespace Common.Contracts.Events.Boards;

public sealed record BoardCreatedIntegrationEvent(
    Guid BoardId,
    string Title,
    string Description,
    string CreatedBy
) : IntegrationEvent;
