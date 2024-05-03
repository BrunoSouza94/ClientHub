namespace ClientHub.Application.Features.Addresses.DeleteAddress;

public sealed record DeleteAddressRequest(Guid Id, Guid ClientId);