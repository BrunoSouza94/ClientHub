using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients.Events;

public record AddressUpdatedDomainEvent(Guid ClientId) : IDomainEvent;