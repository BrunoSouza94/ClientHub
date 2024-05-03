using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients.Events;

public record ClientUpdatedDomainEvent(Guid ClientId) : IDomainEvent;