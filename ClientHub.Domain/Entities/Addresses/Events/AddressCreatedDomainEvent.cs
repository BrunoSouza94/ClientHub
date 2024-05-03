using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients.Events;

public record AddressCreatedDomainEvent(Guid ClientId) : IDomainEvent;