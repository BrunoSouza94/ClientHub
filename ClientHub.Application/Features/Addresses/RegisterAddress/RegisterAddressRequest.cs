namespace ClientHub.Application.Features.Addresses.RegisterAddress;

public sealed record RegisterAddressRequest(
    string Thoroughfare,
    string LocationNumber,
    string Neighborhood,
    string City,
    string State,
    Guid ClientId);