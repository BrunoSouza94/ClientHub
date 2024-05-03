using ClientHub.Application.Features.Clients.RegisterClient;

namespace ClientHub.Application.Features.Clients.UpdateClient;

public sealed record UpdateClientRequest(
    Guid Id,
    string Name,
    string Email,
    List<RegisterAttachedAddressRequest> Addresses)
{
    public string Logo { get; set; }
    public string? FileName { get; set; }
};