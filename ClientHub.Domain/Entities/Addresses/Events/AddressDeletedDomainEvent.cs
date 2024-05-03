using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients.Events;

public record AddressDeletedDomainEvent(Guid ClientId) : IDomainEvent;