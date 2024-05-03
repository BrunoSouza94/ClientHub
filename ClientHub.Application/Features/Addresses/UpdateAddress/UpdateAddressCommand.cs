using ClientHub.Application.Abstractions.Messaging;

namespace ClientHub.Application.Features.Addresses.UpdateAddress;

public record UpdateAddressCommand(
    Guid Id,
    string Thoroughfare,
    string LocationNumber,
    string Neighborhood,
    string City,
    string State,
    Guid ClientId) : ICommand<Guid>;