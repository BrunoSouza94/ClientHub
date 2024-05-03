using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Addresses.Shared;

namespace ClientHub.Application.Features.Addresses.GetAddressById;

public record GetAddressByIdQuery(Guid Id) : IQuery<AddressResponse>;