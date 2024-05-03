using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients.Events;

public record ClientDeletedDomainEvent(Guid ClientId) : IDomainEvent;