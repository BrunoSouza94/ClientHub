using ClientHub.Application.Abstractions.Messaging;

namespace ClientHub.Application.Features.Clients.RegisterClient;

public record UpdateClientCommand(
    Guid Id,
    string Name,
    string Email,
    string Logo,
    List<RegisterAttachedAddressRequest> Addresses) : ICommand<Guid>;