namespace ClientHub.Application.Features.Clients.RegisterClient;

public sealed record RegisterClientRequest(
    string Name,
    string Email,
    List<RegisterAttachedAddressRequest> Addresses)
{
    public string Logo { get; set; }
    public string FileName { get; set; }
};