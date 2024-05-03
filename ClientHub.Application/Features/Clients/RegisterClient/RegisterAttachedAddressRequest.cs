namespace ClientHub.Application.Features.Clients.RegisterClient;

public sealed record RegisterAttachedAddressRequest(
    string Thoroughfare,
    string LocationNumber,
    string Neighborhood,
    string City,
    string State);