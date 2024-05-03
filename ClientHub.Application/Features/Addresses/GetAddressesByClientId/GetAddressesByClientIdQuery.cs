using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Addresses.Shared;

namespace ClientHub.Application.Features.Addresses.GetAddressesByClientId;

public record GetAddressesByClientIdQuery(Guid ClientId) : IQuery<IReadOnlyList<AddressResponse>>;