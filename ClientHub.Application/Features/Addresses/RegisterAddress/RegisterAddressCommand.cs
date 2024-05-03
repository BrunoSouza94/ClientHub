using ClientHub.Application.Abstractions.Messaging;

namespace ClientHub.Application.Features.Addresses.RegisterAddress;

public record RegisterAddressCommand(
    string Thoroughfare,
    string LocationNumber,
    string Neighborhood,
    string City,
    string State,
    Guid ClientId) : ICommand<Guid>;