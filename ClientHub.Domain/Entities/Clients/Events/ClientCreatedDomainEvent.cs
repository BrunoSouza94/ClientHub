using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients.Events;

public record ClientCreatedDomainEvent(Guid ClientId) : IDomainEvent;