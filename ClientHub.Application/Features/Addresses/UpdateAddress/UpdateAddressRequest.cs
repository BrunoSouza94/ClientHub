namespace ClientHub.Application.Features.Addresses.UpdateAddress;

public sealed record UpdateAddressRequest(
    Guid Id,
    string Thoroughfare,
    string LocationNumber,
    string Neighborhood,
    string City,
    string State,
    Guid ClientId);